using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Manager
{
    public class TransactieBeheerder
    {
        private ITransactieRepository repo;

        public TransactieBeheerder(ITransactieRepository repo)
        {
            this.repo = repo;
        }

       

        
    }
}
