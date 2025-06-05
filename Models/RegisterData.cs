using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCD.Models
{
    internal class RegisterData : INotifyPropertyChanged
    {
        private ushort _address;
        private ushort _value1;
        private ushort _value2;

        public ushort Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public ushort Value1
        {
            get => _value1;
            set { _value1 = value; OnPropertyChanged(nameof(Value1)); }
        }

        public ushort Value2
        {
            get => _value2;
            set { _value2 = value; OnPropertyChanged(nameof(Value2)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
