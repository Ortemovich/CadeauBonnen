using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScradaCadeauBonnen_BL.Models;

namespace Scrada_testing
{
    public class CadeaubonTests
    {
        [Fact]
        public void Constructor_Valid()
        {
            var aanmaak = DateTime.Now;
            var verval = aanmaak.AddMonths(6);
            var bon = new Cadeaubon(1, 2, 3, 50, 50, verval, "CODE123", aanmaak, "Actief");

            Assert.Equal(1, bon.CadeaubonId);
            Assert.Equal(2, bon.GebruikerId);
            Assert.Equal(3, bon.ThemaId);
            Assert.Equal(50, bon.Bedrag);
            Assert.Equal(50, bon.Saldo);
            Assert.Equal(verval, bon.VervalDatum);
            Assert.Equal("CODE123", bon.CadeaubonCode);
            Assert.Equal(aanmaak, bon.AanmaakDatum);
            Assert.Equal("Actief", bon.Status);
        }

        [Fact]
        public void EmptyConstructor_Valid()
        {
            var bon = new Cadeaubon();
            bon.Bedrag = 20;
            bon.Status = "Inactief";

            Assert.Equal(20, bon.Bedrag);
            Assert.Equal("Inactief", bon.Status);
        }
    }
}
