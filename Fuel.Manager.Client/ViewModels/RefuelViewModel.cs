using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Models;

namespace Fuel.Manager.Client.ViewModels
{
    public class RefuelViewModel : ViewModelBase
    {
        public ICommand TodayCommand { get; set; }
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Car> EmployeeCars { get; set; }
        private Car _SelectedCar;
        public Car SelectedCar
        {
            get
            {
                return _SelectedCar;
            }

            set
            {
                if (_SelectedCar == value)
                {
                    return;
                }

                _SelectedCar = value;

                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        public ObservableCollection<Refuel> Refuels { get; set; }
        private Refuel _SelectedRefuel;
        public Refuel SelectedRefuel
        {
            get
            {
                return _SelectedRefuel;
            }
            set
            {
                if (_SelectedRefuel == value)
                {
                    return;
                }

                _SelectedRefuel = value;

                if (_SelectedRefuel != null)
                {
                    SelectedCar = _SelectedRefuel.Car;
                    RefuelDate = _SelectedRefuel.Date;
                    Mileage = _SelectedRefuel.Mileage;
                    Amount = _SelectedRefuel.Amount;
                    Price = _SelectedRefuel.Price;
                }

                OnPropertyChanged(nameof(SelectedRefuel));
            }
        }
        public bool _enabled { get; set; }

        public bool enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged("enabled");
            }
        }

        private DateTime _refuelDate;
        public DateTime RefuelDate
        {
            get { return _refuelDate; }
            set
            {
                if (_refuelDate == value)
                {
                    return;
                }
                _refuelDate = value;
                OnPropertyChanged(nameof(RefuelDate));
            }
        }

        private int _mileage;
        public int Mileage
        {
            get { return _mileage; }
            set
            {
                if (_mileage == value)
                {
                    return;
                }
                _mileage = value;
                OnPropertyChanged(nameof(Mileage));
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (_amount == value)
                {
                    return;
                }
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price == value)
                {
                    return;
                }
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private bool _IsEnabled;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (value == _IsEnabled)
                {
                    return;
                }

                _IsEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage == value) return;
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        public RefuelViewModel()
        {
            Cars = new ObservableCollection<Car>();
            Refuels = new ObservableCollection<Refuel>();
            EmployeeCars = new ObservableCollection<Car>();
            RefuelDate = DateTime.Today;
            ErrorMessage = "";
        }
    }
}
