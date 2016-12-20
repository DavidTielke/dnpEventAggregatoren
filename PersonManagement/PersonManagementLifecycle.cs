using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Contract;
using Ninject;

namespace PersonManagement
{
    public class PersonManagementLifecycle : IActivateable, IDeactivateable
    {
        public PersonManagementLifecycle(IKernelker)
        {
            
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }
    }
}
