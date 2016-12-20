using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses;

namespace PersonManagement.Contract
{
    public interface IPersonManager
    {
        IQueryable<Person> Query { get; }
        IQueryable<Person> GetAllAdults();
        IQueryable<Person> GetAllChildren();
    }
}
