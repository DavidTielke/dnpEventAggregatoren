using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataClasses;
using DataClasses.Annotations;
using DataStoring.Contract;
using DataStoring.Contract.Messages;
using EventAggregation.Contract;
using PersonManagement.Contract;

namespace WpfDemo.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IPersonManager _manager;
        private readonly IRepository<Person> _repository;
        private readonly IEventMapper _eventMapper;
        private Person _ausgewähltePerson;

        public MainWindowViewModel(IPersonManager manager, IRepository<Person> repository, IEventMapper eventMapper )
        {
            _manager = manager;
            _repository = repository;
            _eventMapper = eventMapper;
            Personen = new ObservableCollection<Person>(_manager.Query);
            AuditMessages = new ObservableCollection<string>();
            RemovePersonCommand = new RelayCommand(ExecuteRemovePerson, CanExecuteRemovePerson);
            CreatePersonCommand = new RelayCommand(ExecuteCreatePerson);
            ClearPersonsCommand = new RelayCommand(ExecuteClearPersons, CanExecuteClearPersons);
            
            _eventMapper.Map<EntityModifiedMessage>((aggr, msg) =>
            {
                AuditMessages.Add($"{DateTime.Now: dd.MM.yyyy HH:mm:ss} {msg.Status}");
            });
        }

        public ICommand ClearPersonsCommand { get; set; }

        public ICommand CreatePersonCommand { get; set; }

        public ObservableCollection<Person> Personen { get; set; }
        public ICommand RemovePersonCommand { get; set; }

        public ObservableCollection<string> AuditMessages { get; set; }

        public Person AusgewähltePerson
        {
            get { return _ausgewähltePerson; }
            set
            {
                if (Equals(value, _ausgewähltePerson)) return;

                if (_ausgewähltePerson != null)
                {
                    _ausgewähltePerson.PropertyChanged -= AusgewähltePersonWurdeGeändert;
                    _repository.Update(_ausgewähltePerson);
                }

                _ausgewähltePerson = value;

                if (value != null)
                {
                    _ausgewähltePerson.PropertyChanged += AusgewähltePersonWurdeGeändert;
                }

                OnPropertyChanged();
                OnPropertyChanged("KannGelöschtWerden");
                OnPropertyChanged("VornameZeichenVerbleibend");
            }
        }

        public int VornameZeichenVerbleibend
        {
            get
            {
                if (AusgewähltePerson == null || AusgewähltePerson.Vorname == null)
                {
                    return 0;
                }
                return 25 - AusgewähltePerson.Vorname.Length;
            }
        }
        

        private bool CanExecuteClearPersons(object arg)
        {
            return Personen.Count > 0;
        }

        private void ExecuteClearPersons(object obj)
        {
            Personen.Clear();
        }

        private void ExecuteCreatePerson(object selectedPerson)
        {
            var person = new Person();
            Personen.Add(person);
            _repository.Insert(person);
            AusgewähltePerson = person;
        }

        private bool CanExecuteRemovePerson(object selectedPerson)
        {
            return selectedPerson != null;
        }

        private void ExecuteRemovePerson(object obj)
        {
            var personToRemove = obj as Person;
            if (personToRemove == null) return;

            Personen.Remove(personToRemove);
            _repository.Delete(personToRemove.Id);
        }

        private void AusgewähltePersonWurdeGeändert(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Vorname")
            {
                OnPropertyChanged("VornameZeichenVerbleibend");
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
