using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScradaCadeauBonnen_BL.Models;

namespace Scrada_testing
{
    public class GebruikerTests
    {
        [Fact]
        public void Constructor_Valid()
        {
            var datum = DateTime.Now;
            var gebruiker = new Gebruiker(1, "Jan", "Janssens", "jan@example.com", "pass", datum);

            Assert.Equal(1, gebruiker.GebruikerId);
            Assert.Equal("Jan", gebruiker.Naam);
            Assert.Equal("Janssens", gebruiker.Achternaam);
            Assert.Equal("jan@example.com", gebruiker.Email);
            Assert.Equal("pass", gebruiker.Wachtwoord);
            Assert.Equal(datum, gebruiker.AanmaakDatum);
            Assert.False(gebruiker.IsAdmin);
        }

        [Fact]
        public void Constructor_WithoutId_Valid()
        {
            var datum = DateTime.Today;
            var gebruiker = new Gebruiker("Lisa", "Vermeulen", "lisa@mail.com", "123", datum);

            Assert.Equal("Lisa", gebruiker.Naam);
            Assert.Equal("Vermeulen", gebruiker.Achternaam);
            Assert.Equal("lisa@mail.com", gebruiker.Email);
            Assert.Equal("123", gebruiker.Wachtwoord);
            Assert.Equal(datum, gebruiker.AanmaakDatum);
        }
    }
}
