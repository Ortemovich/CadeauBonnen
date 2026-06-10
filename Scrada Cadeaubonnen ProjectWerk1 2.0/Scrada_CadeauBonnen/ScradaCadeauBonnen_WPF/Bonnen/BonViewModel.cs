using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_WPF.Bonnen
{
    public class BonViewModel : INotifyPropertyChanged
    {
        private string _bonCode;
        private decimal _amount;
        private DateTime _validUntil;
        public int ThemaId { get; set; }


        public string BonCode
        {
            get => _bonCode;
            set
            {
                _bonCode = value;
                OnPropertyChanged();
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AmountDisplay));
            }
        }

        public DateTime ValidUntil
        {
            get => _validUntil;
            set
            {
                _validUntil = value;
                OnPropertyChanged();
            }
        }

        public string AmountDisplay => $"€{Amount:F0}";
        public string ValidUntilDisplay => $"Geldig tot: {ValidUntil:dd-MM-yyyy}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BonViewModel()
        {
            BonCode = "ABC123456";
            //Amount = 50;
            ValidUntil = DateTime.Now.AddMonths(12);
        }

    }
}
