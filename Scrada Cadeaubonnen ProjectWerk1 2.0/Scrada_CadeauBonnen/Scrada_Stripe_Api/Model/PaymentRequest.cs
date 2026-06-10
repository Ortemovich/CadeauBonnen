namespace Scrada_Stripe_Api.Model
{
    public class PaymentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public int GebruikerId { get; set; }
        public int CadeaubonId { get; set; }
        public int ThemaId { get; set; }
    }
}
