using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UpaProject.Infrastracture.Commands.Base
{
   internal abstract class Command:ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        //Реализацию методов ниже см. в наследнике
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }
}
