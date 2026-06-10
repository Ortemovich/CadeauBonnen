using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Models;

namespace ScradaCadeauBonnen_BL.Manager
{
    public class GebruikerBeheerder
    {

        private IScradaRepository _repo;

        public GebruikerBeheerder(IScradaRepository repo)
        {
            this._repo = repo;
        }

        public void VoegGebruikerToe(Gebruiker gebruiker)
        {
            _repo.VoegGebruikerToe(gebruiker);
        }

        public Gebruiker GeefGebruikerByEmailEnWachtwoord(string email, string wachtwoord)
        {
            return _repo.GeefGebruikerByEmailEnWachtwoord(email, wachtwoord);
        }

        public Cadeaubon ZoekCadeaubonOpCode(string code)
        {
            return _repo.ZoekCadeaubonOpCode(code);

        }
    }
}
