using ScradaCadeauBonnen_BL.Models;
using ScradaCadeauBonnen_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_WPF.Mappers
{
    public class CadeaubonMapper
    {
        // MapToDomain method
        public static Cadeaubon MapToDomain(CadeaubonUI cadeaubonUI)
        {
            return new Cadeaubon(
                cadeaubonUI.CadeaubonId,
                cadeaubonUI.GebruikerId,
                cadeaubonUI.ThemaId,
                cadeaubonUI.Bedrag,
                cadeaubonUI.Saldo,
                cadeaubonUI.VervalDatum,
                cadeaubonUI.CadeaubonCode,
                cadeaubonUI.AanmaakDatum,
                cadeaubonUI.Status
            );
        }

        // MapToUI method
        public static CadeaubonUI MapToUI(ScradaCadeauBonnen_BL.Models.Cadeaubon cadeaubon)
        {
            return new CadeaubonUI(
                cadeaubon.CadeaubonId,
                cadeaubon.GebruikerId,
                cadeaubon.ThemaId,
                cadeaubon.Bedrag,
                cadeaubon.Saldo,
                cadeaubon.VervalDatum,
                cadeaubon.CadeaubonCode,
                cadeaubon.AanmaakDatum,
                cadeaubon.Status
            );
        }
    }
}
