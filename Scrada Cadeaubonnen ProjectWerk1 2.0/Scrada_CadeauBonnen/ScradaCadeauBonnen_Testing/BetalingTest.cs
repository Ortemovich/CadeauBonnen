using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_Testing
{
    public class BetalingTests
    {
        [Fact]
        public void Constructor_()
        {
            var datum = DateTime.Now;
            var betaling = new Betaling(1, 10, 100, "stripe123", "Stripe", datum, 25.50m);

            Assert.Equal(1, betaling.BetalingId);
            Assert.Equal(10, betaling.GebruikerId);
            Assert.Equal(100, betaling.CadeaubonId);
            Assert.Equal("stripe123", betaling.StripeBetalingId);
            Assert.Equal("Stripe", betaling.BetaalMethode);
            Assert.Equal(datum, betaling.BetalingDatum);
            Assert.Equal(25.50m, betaling.Bedrag);
        }
    }
}
