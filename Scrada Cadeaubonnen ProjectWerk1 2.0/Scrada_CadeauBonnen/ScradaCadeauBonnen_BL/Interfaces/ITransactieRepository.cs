using ScradaCadeauBonnen_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_BL.Interfaces
{
    public interface ITransactieRepository
    {
        void VerwerkVolledigeBetaling(Cadeaubon cadeaubon, Betaling betaling, Transactie transactie);

    }
}
