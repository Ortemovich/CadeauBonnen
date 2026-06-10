using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_WPF.Models
{
    public class CadeaubonUI : INotifyPropertyChanged
    {
        private int _cadeaubonId;
        private int _gebruikerId;
        private int _themaId;
        private decimal _bedrag;
        private decimal _saldo;
        private DateTime _vervalDatum;
        private string _cadeaubonCode;
        private DateTime _aanmaakDatum;
        private string _status;

        public int CadeaubonId
        {
            get => _cadeaubonId;
            set
            {
                if (_cadeaubonId != value)
                {
                    _cadeaubonId = value;
                    OnPropertyChanged(nameof(CadeaubonId));
                }
            }
        }

        public int GebruikerId
        {
            get => _gebruikerId;
            set
            {
                if (_gebruikerId != value)
                {
                    _gebruikerId = value;
                    OnPropertyChanged(nameof(GebruikerId));
                }
            }
        }

        public int ThemaId
        {
            get => _themaId;
            set
            {
                if (_themaId != value)
                {
                    _themaId = value;
                    OnPropertyChanged(nameof(ThemaId));
                }
            }
        }

        public decimal Bedrag
        {
            get => _bedrag;
            set
            {
                if (_bedrag != value)
                {
                    _bedrag = value;
                    OnPropertyChanged(nameof(Bedrag));
                }
            }
        }

        public decimal Saldo
        {
            get => _saldo;
            set
            {
                if (_saldo != value)
                {
                    _saldo = value;
                    OnPropertyChanged(nameof(Saldo));
                }
            }
        }

        public DateTime VervalDatum
        {
            get => _vervalDatum;
            set
            {
                if (_vervalDatum != value)
                {
                    _vervalDatum = value;
                    OnPropertyChanged(nameof(VervalDatum));
                }
            }
        }

        public string CadeaubonCode
        {
            get => _cadeaubonCode;
            set
            {
                if (_cadeaubonCode != value)
                {
                    _cadeaubonCode = value;
                    OnPropertyChanged(nameof(CadeaubonCode));
                }
            }
        }

        public DateTime AanmaakDatum
        {
            get => _aanmaakDatum;
            set
            {
                if (_aanmaakDatum != value)
                {
                    _aanmaakDatum = value;
                    OnPropertyChanged(nameof(AanmaakDatum));
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public CadeaubonUI(int cadeaubonId, int gebruikerId, int themaId, decimal bedrag, decimal saldo, DateTime vervalDatum, string cadeaubonCode, DateTime aanmaakDatum, string status)
        {
            CadeaubonId = cadeaubonId;
            GebruikerId = gebruikerId;
            ThemaId = themaId;
            Bedrag = bedrag;
            Saldo = saldo;
            VervalDatum = vervalDatum;
            CadeaubonCode = cadeaubonCode;
            AanmaakDatum = aanmaakDatum;
            Status = status;
        }

        public CadeaubonUI()
        {
        }

        public CadeaubonUI(decimal bedrag)
        {
            Bedrag = bedrag;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Cadeaubon {Bedrag} euro";
        }
    }
}
