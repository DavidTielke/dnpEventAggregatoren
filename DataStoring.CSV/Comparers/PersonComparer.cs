using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataClasses;

namespace DataStoring.CSV.Comparers
{
    class PersonEqualityComparer : IEqualityComparer
    {
        public bool Equals(object x, object y)
        {
            return GetHashCode(x) == GetHashCode(y);
        }

        public int GetHashCode(object obj)
        {
            var p = (Person) obj;
            return $"{p.Id};{p.IsMale};{p.Age};{p.Vorname};{p.Nachname}".GetHashCode();
        }
    }
}
