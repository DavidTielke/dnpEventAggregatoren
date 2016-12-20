using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDemo
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeCallback;
        private readonly Func<object, bool> _canExecuteCallback;

        public RelayCommand(Action<object> executeCallback)
        {
            _executeCallback = executeCallback;
        }

        public RelayCommand(Action<object> executeCallback, Func<object, bool> canExecuteCallback)
            : this(executeCallback)
        {
            _canExecuteCallback = canExecuteCallback;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteCallback == null)
                return true;
            return _canExecuteCallback(parameter);
        }

        public void Execute(object parameter)
        {
            _executeCallback(parameter);
        }


        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
