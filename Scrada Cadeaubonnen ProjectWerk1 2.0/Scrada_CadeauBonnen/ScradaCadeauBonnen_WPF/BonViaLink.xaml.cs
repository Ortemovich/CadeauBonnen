using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScradaCadeauBonnen_BL.Manager;
using ScradaCadeauBonnen_WPF.Bonnen;

namespace ScradaCadeauBonnen_WPF
{
    /// <summary>
    /// Interaction logic for BonViaLink.xaml
    /// </summary>
    public partial class BonViaLink : Window
    {
        private readonly GebruikerBeheerder _beheerder;

        public BonViaLink(string bonCode, GebruikerBeheerder beheerder)
        {
            InitializeComponent();
            _beheerder = beheerder;

            var cadeaubon = _beheerder.ZoekCadeaubonOpCode(bonCode);
            if (cadeaubon == null)
            {
                MessageBox.Show("Geen bon gevonden met deze code.");
                Close();
                return;
            }

            txtCode.Text = $"Code: {cadeaubon.CadeaubonCode}";
            txtBedrag.Text = $"Bedrag: €{cadeaubon.Bedrag}";
            txtGeldigTot.Text = $"Geldig tot: {cadeaubon.VervalDatum:dd-MM-yyyy}";

            switch (cadeaubon.ThemaId)
            {
                case 1:
                    ThemaPreview.Content = new Bonnen.FancyBon
                    {
                        DataContext = new BonViewModel
                        {
                            BonCode = cadeaubon.CadeaubonCode,
                            Amount = cadeaubon.Bedrag,
                            ValidUntil = cadeaubon.VervalDatum,
                            ThemaId = cadeaubon.ThemaId
                        }
                    };
                    break;

                case 2:
                    ThemaPreview.Content = new Bonnen.ModerneBon
                    {
                        DataContext = new BonViewModel
                        {
                            BonCode = cadeaubon.CadeaubonCode,
                            Amount = cadeaubon.Bedrag,
                            ValidUntil = cadeaubon.VervalDatum,
                            ThemaId = cadeaubon.ThemaId
                        }
                    };
                    break;

                case 3:
                    ThemaPreview.Content = new Bonnen.FeestelijkeBon
                    {
                        DataContext = new BonViewModel
                        {
                            BonCode = cadeaubon.CadeaubonCode,
                            Amount = cadeaubon.Bedrag,
                            ValidUntil = cadeaubon.VervalDatum,
                            ThemaId = cadeaubon.ThemaId
                        }
                    };
                    break;
            }
        }
    }
}
