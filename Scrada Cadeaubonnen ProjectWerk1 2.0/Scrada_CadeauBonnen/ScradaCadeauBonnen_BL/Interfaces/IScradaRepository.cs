using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScradaCadeauBonnen_BL.Models;

namespace ScradaCadeauBonnen_BL.Interfaces
{
    public interface IScradaRepository
    {
        void VoegGebruikerToe(Gebruiker gebruiker);

        Gebruiker GeefGebruikerByEmailEnWachtwoord(string email, string wachtwoord);

        public Cadeaubon ZoekCadeaubonOpCode(string code);
    }
}
