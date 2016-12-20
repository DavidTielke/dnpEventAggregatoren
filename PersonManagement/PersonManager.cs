using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Contracts;
using DataClasses;
using DataStoring.Contract;
using PersonManagement.Contract;

namespace PersonManagement
{
    public class PersonManager : IPersonManager
    {
        private readonly IRepository<Person> _repository;
        private int _ageTreshold;

        public PersonManager(IRepository<Person> repository, IConfigurator config )
        {
            _repository = repository;
            _ageTreshold = config.Get("PersonManagement", "AgeTreshold", 18);
        }

        public IQueryable<Person> Query => _repository.Query;

        public IQueryable<Person> GetAllAdults()
        {
            return _repository.Query.Where(p => p.Age >= _ageTreshold);
        }

        public IQueryable<Person> GetAllChildren()
        {
            return _repository.Query.Where(p => p.Age < _ageTreshold);
        }
    }
}
