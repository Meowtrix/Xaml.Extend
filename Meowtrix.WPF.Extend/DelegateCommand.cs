using System;
using System.Windows.Input;

namespace Meowtrix.WPF.Extend
{
    public class DelegateCommand : ICommand
    {
        public Action<object> Action { get; }
        public DelegateCommand(Action<object> action)
        {
            Action = action;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => Action(parameter);
    }
}
