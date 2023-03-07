using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lopushok1.Commands
{
    public class Command : ICommand
    {
        protected Action<object> _action;
        protected Predicate<object> _predicate;
        public event EventHandler? CanExecuteChanged;

        public Command(Action<object> action)
        {
            this._action = action;
        }

        public bool CanExecute(object? parameter)
        {
            if (this._predicate == null)
                return true;

            return this._predicate(parameter);
        }

        public void Execute(object? parameter)
        {
            this._action(parameter);
        }
    }
}
