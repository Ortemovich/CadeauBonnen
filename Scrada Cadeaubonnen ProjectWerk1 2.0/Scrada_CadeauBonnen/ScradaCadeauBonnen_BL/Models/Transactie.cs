using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Models
{
    public class Transactie
    {
        public Transactie()
        {
        }

        public Transactie(int transactieId, int cadeaubonId, decimal bedrag, DateTime datum, string reden, string status)
        {
            TransactieId = transactieId;
            CadeaubonId = cadeaubonId;
            Bedrag = bedrag;
            Datum = datum;
            Reden = reden;
            Status = status;
        }

        public Transactie(int transactieId, int cadeaubonId, decimal bedrag, DateTime datum, string reden, string status, string stripeBetalingId)
        {
            TransactieId = transactieId;
            CadeaubonId = cadeaubonId;
            Bedrag = bedrag;
            Datum = datum;
            Reden = reden;
            Status = status;
            StripeBetalingId = stripeBetalingId;
        }

        public int TransactieId { get; set; }
        public int CadeaubonId { get; set; }
        public decimal Bedrag { get; set; }
        public DateTime Datum { get; set; }
        public string Reden { get; set; }
        public string Status { get; set; }
        public string StripeBetalingId { get; set; }
    }
}
