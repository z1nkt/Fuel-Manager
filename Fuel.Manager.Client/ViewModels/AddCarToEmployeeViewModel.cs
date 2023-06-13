using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuel.Manager.Client.Framework;
using System.Windows.Input;
using Fuel.Manager.Client.Models;

namespace Fuel.Manager.Client.ViewModels
{
    public class AddCarToEmployeeViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ObservableCollection<Car> Cars { get; set; }
        private Car _SelectedCar;

        public Car SelectedCar
        {
            get { return _SelectedCar; }
            set
            {
                if (_SelectedCar == value)
                {
                    return;
                }

                _SelectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }

        public AddCarToEmployeeViewModel()
        {
            Cars = new ObservableCollection<Car>();
        }
    }
    
}
