using Scrada_Stripe_Api.Model;
using Stripe;
using Stripe.Checkout;

namespace Scrada_Stripe_Api.Services
{
    public class StripeService
    {
        public StripeService(IConfiguration configuration)
        {
            var apiKey = configuration["Stripe:SecretKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("Stripe API key is not configured. Set Stripe:SecretKey or env variable Stripe__SecretKey.");
            }

            StripeConfiguration.ApiKey = apiKey;
        }

        public PaymentIntent CreatePaymentIntent(long amountInCents, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            return service.Create(options);
        }

        public List<PaymentIntent> GetAllPaymentIntents()
        {
            var service = new PaymentIntentService();
            var options = new PaymentIntentListOptions
            {
                Limit = 100,
            };
            var paymentIntents = service.List(options);
            return paymentIntents.ToList();
        }

        public Session CreateCheckoutSession(long amountInCents, string currency, int gebruikerId, int cadeaubonId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = currency,
                    UnitAmount = amountInCents,
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
            { "gebruikerId", gebruikerId.ToString() },
            { "cadeaubonId", cadeaubonId.ToString() }
        },
                Mode = "payment",
                SuccessUrl = "https://localhost:7124/betaling/success",
                CancelUrl = "https://localhost:7124/betaling/cancel"
            };

            var service = new SessionService();
            return service.Create(options);
        }

    }
}
