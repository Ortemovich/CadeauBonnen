using ScradaCadeauBonnen_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Interfaces
{
    public interface IAdminRepository
    {
        List<Gebruiker> GetGebruikers();
        List<Cadeaubon> GetBonnen();
        List<Transactie> GetTransacties();
        Gebruiker AuthenticateAdmin(string email, string hashedPassword);
    }
}
