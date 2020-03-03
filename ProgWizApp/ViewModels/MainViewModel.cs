using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Michalski
{
    class MainViewModel
    {
        public ObservableCollection<Violin> violins;
        private ICommand newViolinCmd;
        public ICommand NewViolinCmd
        {
            get {
                if (newViolinCmd == null) newViolinCmd = new Commands.NewViolinCmd();
                return newViolinCmd;
            }
            set {
                newViolinCmd = value;
            }
        }

        public MainViewModel()
        {
            violins = new ObservableCollection<Violin>();
        }
    }
}
