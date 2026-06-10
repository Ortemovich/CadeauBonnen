using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }
        public string Naam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public bool IsAdmin { get; set; } = false; 

        public Gebruiker(int gebruikerId, string naam, string achternaam, string email, string wachtwoord, DateTime aanmaakDatum)
        {
            GebruikerId = gebruikerId;
            Naam = naam;
            Achternaam = achternaam;
            Email = email;
            Wachtwoord = wachtwoord;
            AanmaakDatum = aanmaakDatum;
        }

        public Gebruiker(string naam, string achternaam, string email, string wachtwoord, DateTime aanmaakDatum)
        {
            
            Naam = naam;
            Achternaam = achternaam;
            Email = email;
            Wachtwoord = wachtwoord;
            AanmaakDatum = aanmaakDatum;
        }

        public Gebruiker()
        {
        }
    }
}
