using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fuel.Manager.Client.Framework;

namespace Fuel.Manager.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase mActiveViewModel;

        public ICommand OpenEmployeeCommand { get; set; }
        public ICommand OpenCarCommand { get; set; }

        public ViewModelBase ActiveViewModel
        {
            get { return mActiveViewModel; }
            set
            {
                if(mActiveViewModel == value) return;
                mActiveViewModel = value;
                OnPropertyChanged("ActiveViewModel");
            }
        }
    }
}
