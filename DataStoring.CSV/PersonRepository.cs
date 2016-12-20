using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Contracts;
using DataClasses;
using DataStoring.Contract;
using DataStoring.Contract.Exceptions;
using DataStoring.Contract.Messages;
using DataStoring.CSV.Comparers;
using EventAggregation.Contract;

namespace DataStoring.CSV
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly string _csvFilePath;
        private List<Person> _persons;
        // Todo: DataStoringConfigurator einbauen
        public PersonRepository(IConfigurator config, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _csvFilePath = config.Get("DataStoring", "CsvFilePath", "data.csv");
            LoadAll();
        }

        private void RaiseEntityModifiedMessage(Person entity, string status)
        {
            var message = new EntityModifiedMessage
            {
                Sender = this,
                Entity = entity,
                Status = status
            };
            _eventAggregator.Raise(this, message);
        }

        private void LoadAll()
        {
            var persons = File.ReadAllLines(_csvFilePath)
                .Select(line => line.Split(new[] {","}, StringSplitOptions.None))
                .Select(parts => new Person
                {
                    Id = int.Parse(parts[0]),
                    Vorname = parts[1],
                    Nachname = parts[2],
                    IsMale = bool.Parse(parts[3]),
                    Age = int.Parse(parts[4])
                }).ToList();
            _persons = persons;
        }

        private void StoreAll()
        {
            var stringBuilder = new StringBuilder();
            foreach (var person in _persons)
            {
                stringBuilder.AppendLine($"{person.Id},{person.Vorname},{person.Nachname},{person.IsMale},{person.Age}");
            }
            var data = stringBuilder.ToString();

            File.WriteAllText(_csvFilePath, data);
        }

        private int GetIndex(int id)
        {
            var itemInStorage = _persons.Any(p => p.Id == id);
            if (!itemInStorage)
            {
                throw new DataStoringException($"No item with id {id} in storage.");
            }

            var indexOfItem = _persons.FindIndex(p => p.Id == id);
            return indexOfItem;
        }

        private int GetIndex(Person item)
        {
            var indexOfItem = GetIndex(item.Id);
            return indexOfItem;
        }

        public void Insert(Person item)
        {
            // Todo: Ungünstige Implementierung, da key mehrfach vergeben werden kann
            var newId = _persons.Max(p => p.Id) + 1;
            item.Id = newId;
            _persons.Add(item);
            StoreAll();
            RaiseEntityModifiedMessage(item, "Person inserted into csv storage");
        }

        public void Update(Person item)
        {
            var indexOfItem = GetIndex(item);
            
            var equalityComparer = new PersonEqualityComparer();
            var objectHasChanged = !equalityComparer.Equals(item, _persons[indexOfItem]);
            if (!objectHasChanged)
            {
                return;
            }

            _persons[indexOfItem] = item;
            StoreAll();
            RaiseEntityModifiedMessage(item, "Person updated in csv storage");
        }

        public void Delete(int id)
        {
            var indexOfItem = GetIndex(id);
            var item = _persons[indexOfItem];
            _persons.RemoveAt(indexOfItem);
            StoreAll();
            RaiseEntityModifiedMessage(item, "Person removed from csv storage");
        }

        public IQueryable<Person> Query => _persons.AsQueryable();
    }
}
