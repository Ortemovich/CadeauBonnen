using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Manager
{
    public class AdminBeheerder  //:IAdminBeheerder //TODO: Dit kan niet de beheerder kan niet overerven van een interface je moet hier Dependency Injection gebruiken om de repository te injecteren
    {

        private readonly IAdminRepository _repo;

        public AdminBeheerder(IAdminRepository repo)
        {
            _repo = repo;
        }
        
        public List<Cadeaubon> GetBonnen()
        {
            return _repo.GetBonnen();
        }

        public List<Gebruiker> GetGebruikers()
        {
            return _repo.GetGebruikers();
        }

        public List<Transactie> GetTransacties()
        {
           return _repo.GetTransacties();
        }

        public Gebruiker AuthenticateAdmin(string email, string hashedPassword)
        {
            return _repo.AuthenticateAdmin(email, hashedPassword);
        }
    }
}
