using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assignment_1a.ViewModels.Commands
{
    public class ActionCommand : ICommand
    {
        private readonly Action _execute;


        private readonly Func<bool> _canExecute;//instead of prev line 

       
        public ActionCommand(Action execute)//instead of prev line
        {
            _execute = execute;
        }

        //public Command(Action<object> execute,
        public ActionCommand(Action execute,//instead of prev line 
        Func<bool> canExecute)//added instead of next line 
                              //Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _execute();//added instead of prev line 
        }
        public bool CanExecute(object parameter)
        {
            return (_canExecute == null)     
            || _canExecute();//added instead of prev line 
        }
        public event EventHandler CanExecuteChanged = delegate { };
        public void RaiseCanExecuteChanged()
        { CanExecuteChanged(this, new EventArgs()); }
    }
}
