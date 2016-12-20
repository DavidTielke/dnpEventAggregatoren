using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Contracts
{
    public interface IConfigurator
    {
        TType Get<TType>(string category, string key, TType defaultValue = default(TType));
        void Set<TType>(string category, string key, TType value);
    }
}
