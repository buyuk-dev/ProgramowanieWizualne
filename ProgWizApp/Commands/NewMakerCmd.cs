using System;
using System.Windows.Input;

namespace Michalski.Commands
{
    internal class NewMakerCmd : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Add New Maker Command");
        }
    }
}