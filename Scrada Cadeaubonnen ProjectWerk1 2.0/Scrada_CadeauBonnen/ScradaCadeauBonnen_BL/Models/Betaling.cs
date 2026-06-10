using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Models
{
    public class Betaling
    {
        public int BetalingId { get; set; }
        public int GebruikerId { get; set; }
        public int CadeaubonId { get; set; }
        public string StripeBetalingId { get; set; }
        public string BetaalMethode { get; set; }
        public DateTime BetalingDatum { get; set; }
        public decimal Bedrag { get; set; }

        // Constructor waar BetalingId optioneel is
        public Betaling(int betalingId, int gebruikerId, int cadeaubonId, string stripeBetalingId, string betaalMethode, DateTime betalingDatum, decimal bedrag)
        {
            BetalingId = betalingId;
            GebruikerId = gebruikerId;
            CadeaubonId = cadeaubonId;
            StripeBetalingId = stripeBetalingId;
            BetaalMethode = betaalMethode;
            BetalingDatum = betalingDatum;
            Bedrag = bedrag;
        }


    }
}
