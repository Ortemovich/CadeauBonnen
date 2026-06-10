using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Scrada_Stripe_Api.Model;
using Scrada_Stripe_Api.Services;
using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Models;
using Stripe;
using Stripe.Checkout;
using System.Text.Json;

namespace Scrada_Stripe_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly StripeService _stripeService;
        private readonly ITransactieRepository _repo;
        private readonly string _webhookSecret;

        public PaymentController(StripeService stripeService, ITransactieRepository repo, IConfiguration config)
        {
            _stripeService = stripeService;
            _repo = repo;
            _webhookSecret = config["Stripe:WebhookSecret"]; // Zet in appsettings.json
        }

        [HttpPost("create-payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentRequest request)
        {
            if (request == null || request.Amount <= 0)
                return BadRequest("Invalid payment request.");

            var paymentIntent = _stripeService.CreatePaymentIntent(request.Amount, request.Currency);
            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] PaymentRequest request)
        {
            if (request == null || request.Amount <= 0 || string.IsNullOrEmpty(request.Currency))
                return BadRequest("Invalid payment request.");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = request.Currency,
                            UnitAmount = request.Amount,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Cadeaubon"
                            }
                        },
                        Quantity = 1
                    }
                },
                Metadata = new Dictionary<string, string>
                {
                    { "gebruikerId", request.GebruikerId.ToString() },
                    { "cadeaubonId", request.CadeaubonId.ToString() },
                    { "themaId", request.ThemaId.ToString() } 
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:7124/betaling/success",
                CancelUrl = "https://localhost:7124/betaling/cancel"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(new { url = session.Url });
        }

        [HttpPost("process-payment")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest request)
        {
            if (request == null || request.Amount <= 0 || string.IsNullOrEmpty(request.Currency))
                return BadRequest("Invalid payment request.");

            try
            {
                var session = _stripeService.CreateCheckoutSession(request.Amount, request.Currency, request.GebruikerId, request.CadeaubonId);
                return Ok(new { sessionUrl = session.Url });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating session: {ex.Message}");
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _webhookSecret
                );

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;

                    if (session == null || session.Metadata == null)
                        return BadRequest("Missing session metadata.");

                    var gebruikerId = int.Parse(session.Metadata["gebruikerId"]);
                    var bedrag = session.AmountTotal.GetValueOrDefault() / 100m;

                    // 1. Maak Cadeaubon aan (BL-model)
                    var cadeaubon = new Cadeaubon
                    {
                        GebruikerId = gebruikerId,
                        ThemaId = int.Parse(session.Metadata["themaId"]), // Pas aan indien nodig
                        Bedrag = bedrag,
                        Saldo = bedrag,
                        VervalDatum = DateTime.Now.AddYears(1),
                        CadeaubonCode = Guid.NewGuid().ToString().Substring(0, 12),
                        AanmaakDatum = DateTime.Now,
                        Status = "Actief"
                    };

                    // 2. Maak Betaling aan (CadeaubonId wordt in repo gezet)
                    var betaling = new Betaling(
                        betalingId: 0,
                        gebruikerId: gebruikerId,
                        cadeaubonId: 0,
                        stripeBetalingId: session.Id,
                        betaalMethode: "Stripe",
                        betalingDatum: DateTime.Now,
                        bedrag: bedrag
                    );

                    // 3. Maak Transactie aan (CadeaubonId wordt in repo gezet)
                    var transactie = new Transactie(
                        transactieId: 0,
                        cadeaubonId: 0,
                        bedrag: bedrag,
                        datum: DateTime.Now,
                        reden: "Stripe betaling voltooid via webhook",
                        status: "Completed",
                        stripeBetalingId: session.Id
                    );

                    // 4. Alles opslaan in één transactie
                     _repo.VerwerkVolledigeBetaling(cadeaubon, betaling, transactie);

                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest($"Webhook error: {e.Message}");
            }
        }

    }
}
