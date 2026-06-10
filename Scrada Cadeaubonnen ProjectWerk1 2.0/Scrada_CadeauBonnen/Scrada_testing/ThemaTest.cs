using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScradaCadeauBonnen_BL.Models;

namespace Scrada_testing
{
    public class ThemaTests
    {
        [Fact]
        public void Constructor_Valid()
        {
            var thema = new Thema(1, "Modern", "#FFFFFF", "img.png", 6);

            Assert.Equal(1, thema.ThemaId);
            Assert.Equal("Modern", thema.Naam);
            Assert.Equal("#FFFFFF", thema.Kleur);
            Assert.Equal("img.png", thema.Afbeelding);
            Assert.Equal(6, thema.GeldigheidsduurInMaanden);
        }
    }
}
