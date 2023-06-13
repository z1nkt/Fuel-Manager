using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fuel.Manager.Client.Controllers;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Models;

namespace Fuel.Manager.Client.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        public SecureString SecurePassword { private get; set; }
        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            Cars = new ObservableCollection<Car>();
        }

        public EmployeeController EmployeeController { get; set; }

        public ICommand AddCarCommand { get; set; }
        public ICommand RemoveCarCommand { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }
        private Employee _SelectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set
            {
                if (_SelectedEmployee == value)
                {
                    return;
                }
                _SelectedEmployee = value;
                if (_SelectedEmployee != null)
                {
                    //updates the detail fields to the values of the selected item
                    EmployeeNo = _SelectedEmployee.EmployeeNo;
                    Username = _SelectedEmployee.Username;
                    Firstname = _SelectedEmployee.Firstname;
                    Lastname = _SelectedEmployee.Lastname;
                    IsAdmin = _SelectedEmployee.IsAdmin;
                    Password = _SelectedEmployee.Password;

                    //updates the list of cars
                    EmployeeController.GetCarsFromEmployeeID(_SelectedEmployee.Id);
                }

                OnPropertyChanged("SelectedEmployee");
            }
        }

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

        private string _EmployeeNo;
        public string EmployeeNo
        {
            get { return _EmployeeNo; }
            set
            {
                if (value == _EmployeeNo)
                {
                    return;
                }
                _EmployeeNo = value;
                OnPropertyChanged("EmployeeNo");
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                if (value == _Username)
                {
                    return;
                }
                _Username = value;
                OnPropertyChanged("Username");
            }
        }

        private string _Firstname;
        public string Firstname
        {
            get { return _Firstname; }
            set
            {
                if (value == _Firstname)
                {
                    return;
                }
                _Firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        private string _Lastname;
        public string Lastname
        {
            get { return _Lastname; }
            set
            {
                if (value == _Lastname)
                {
                    return;
                }
                _Lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        private bool _IsAdmin;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                if (value == _IsAdmin)
                {
                    return;
                }
                _IsAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set
            {
                if (value == _Password)
                {
                    return;
                }
                _Password = value;
                OnPropertyChanged(nameof(Password));
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
    }
}
