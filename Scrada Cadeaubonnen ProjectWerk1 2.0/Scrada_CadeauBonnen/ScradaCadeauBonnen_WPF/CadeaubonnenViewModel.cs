using ScradaCadeauBonnen_WPF.Bonnen;
using ScradaCadeauBonnen_BL.Manager;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ScradaCadeauBonnen_WPF
{
    public class CadeaubonnenViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BonViewModel> Cadeaubonnen { get; set; }
        private ObservableCollection<string> _bestellingInfo;
        public ObservableCollection<string> BestellingInfo
        {
            get => _bestellingInfo;
            set
            {
                _bestellingInfo = value;
                OnPropertyChanged();
            }
        }

        private Cadeaubon _selectedCadeaubon;
        public Cadeaubon SelectedCadeaubon
        {
            get => _selectedCadeaubon;
            set
            {
                _selectedCadeaubon = value;
                OnPropertyChanged();
            }
        }

        private string _bedrag;
        public string Bedrag
        {
            get => _bedrag;
            set
            {
                _bedrag = value;
                OnPropertyChanged();
            }
        }

        private string _reden;
        public string Reden
        {
            get => _reden;
            set
            {
                _reden = value;
                OnPropertyChanged();
            }
        }

        public CadeaubonnenViewModel()
        {
            var service = new CadeaubonBeheerder(); 

            var Bonnen = service.GetBonnenByGebruikerId(1); 
            //die 1 is hardcoded nu dat zou eigenlijk moeten komen van
            //de id van de gebruiker van de huidige sessie

            Cadeaubonnen = new ObservableCollection<BonViewModel>(
                        Bonnen.Select(b => new BonViewModel
                    {
                        BonCode = b.CadeaubonCode,
                        Amount = b.Saldo,
                        ValidUntil = b.VervalDatum,
                        ThemaId = b.ThemaId
                     })
                        );

        }



        private void ExecuteGebruik(object parameter)
        {
            if (SelectedCadeaubon != null && decimal.TryParse(Bedrag, out var amount))
            {
                SelectedCadeaubon.Total -= amount;
                // Save reden somewhere 
                Reden = string.Empty; 
                Bedrag = string.Empty; 
            }
        }

        private bool CanExecuteGebruik(object parameter)
        {
            return SelectedCadeaubon != null && decimal.TryParse(Bedrag, out var amount) && amount > 0 && amount <= SelectedCadeaubon.Total;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Cadeaubon : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private decimal _total;
        private int cadeaubonId;
        private int gebruikerId;
        private int themaId;
        private decimal bedrag;
        private decimal saldo;
        private DateTime vervalDatum;
        private string cadeaubonCode;
        private DateTime aanmaakDatum;
        private string status;

        public Cadeaubon(int cadeaubonId, int gebruikerId, int themaId, decimal bedrag, decimal saldo, DateTime vervalDatum, string cadeaubonCode, DateTime aanmaakDatum, string status)
        {
            this.cadeaubonId = cadeaubonId;
            this.gebruikerId = gebruikerId;
            this.themaId = themaId;
            this.bedrag = bedrag;
            this.saldo = saldo;
            this.vervalDatum = vervalDatum;
            this.cadeaubonCode = cadeaubonCode;
            this.aanmaakDatum = aanmaakDatum;
            this.status = status;
        }

        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
}
