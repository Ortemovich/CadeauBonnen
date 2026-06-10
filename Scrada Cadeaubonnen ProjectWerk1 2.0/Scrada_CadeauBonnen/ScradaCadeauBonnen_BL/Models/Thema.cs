using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Models
{
    public class Thema
    {
        public int ThemaId { get; set; }
        public string Naam { get; set; }
        public string Kleur { get; set; }
        public string Afbeelding { get; set; }
        public int? GeldigheidsduurInMaanden { get; set; }

        public Thema(int themaId, string naam, string kleur, string afbeelding, int? geldigheidsduurInMaanden)
        {
            ThemaId = themaId;
            Naam = naam;
            Kleur = kleur;
            Afbeelding = afbeelding;
            GeldigheidsduurInMaanden = geldigheidsduurInMaanden;
        }
    }
}
