using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Contract;

namespace Bootstrapper
{
    public class Bootstrapper : IBootstrapper
    {
        private readonly IActivateable[] _activateables;
        private readonly IDeactivateable[] _deactivateables;

        public Bootstrapper(IActivateable[] activateables, IDeactivateable[] deactivateables)
        {
            _activateables = activateables;
            _deactivateables = deactivateables;
        }

        public void ActivateAll()
        {
            foreach (var activateable in _activateables)
            {
                activateable.Activate();
            }
        }

        public void DeactivateAll()
        {
            foreach (var deactivateable in _deactivateables)
            {
                deactivateable.Deactivate();
            }
        }
    }
}
