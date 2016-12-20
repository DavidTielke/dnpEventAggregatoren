using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataClasses.Annotations;

namespace DataClasses
{
    public class Person : IIdentifyableEntity, INotifyPropertyChanged
    {
        private int _id;
        private string _vorname;
        private string _nachname;
        private int _age;
        private bool _isMale;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (value == _vorname) return;
                _vorname = value;
                OnPropertyChanged();
            }
        }

        public string Nachname
        {
            get { return _nachname; }
            set
            {
                if (value == _nachname) return;
                _nachname = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (value == _age) return;
                _age = value;
                OnPropertyChanged();
            }
        }

        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                if (value == _isMale) return;
                _isMale = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
