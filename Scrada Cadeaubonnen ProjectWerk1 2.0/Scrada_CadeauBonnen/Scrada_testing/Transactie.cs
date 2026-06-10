using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScradaCadeauBonnen_BL.Models;

namespace Scrada_testing
{
    public class TransactieTests
    {
        [Fact]
        public void Constructor_Valid()
        {
            var datum = DateTime.Today;
            var transactie = new Transactie(1, 10, 100.00m, datum, "Aankoop", "Compleet");

            Assert.Equal(1, transactie.TransactieId);
            Assert.Equal(10, transactie.CadeaubonId);
            Assert.Equal(100.00m, transactie.Bedrag);
            Assert.Equal(datum, transactie.Datum);
            Assert.Equal("Aankoop", transactie.Reden);
            Assert.Equal("Compleet", transactie.Status);
            Assert.Null(transactie.StripeBetalingId);
        }

        [Fact]
        public void Constructor_WithStripeId_Valid()
        {
            var datum = DateTime.Now;
            var transactie = new Transactie(2, 20, 200.00m, datum, "Vergoeding", "In behandeling", "stripe123");

            Assert.Equal("stripe123", transactie.StripeBetalingId);
            Assert.Equal("Vergoeding", transactie.Reden);
        }
    }
}
