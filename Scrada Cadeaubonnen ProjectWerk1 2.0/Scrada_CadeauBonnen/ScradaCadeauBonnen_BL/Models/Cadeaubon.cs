using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Models
{
    public class Cadeaubon
    {
        public int CadeaubonId { get; set; }
        public int GebruikerId { get; set; }
        public int ThemaId { get; set; }
        public decimal Bedrag { get; set; }
        public decimal Saldo { get; set; }
        public DateTime VervalDatum { get; set; }
        public string CadeaubonCode { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public string Status { get; set; }

        public Cadeaubon(int cadeaubonId, int gebruikerId, int themaId, decimal bedrag, decimal saldo, DateTime vervalDatum, string cadeaubonCode, DateTime aanmaakDatum, string status)
        {
            CadeaubonId = cadeaubonId;
            GebruikerId = gebruikerId;
            ThemaId = themaId;
            Bedrag = bedrag;
            Saldo = saldo;
            VervalDatum = vervalDatum;
            CadeaubonCode = cadeaubonCode;
            AanmaakDatum = aanmaakDatum;
            Status = status;
        }

        public Cadeaubon()
        {
        }
    }
}
