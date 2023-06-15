using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Models;
using Fuel.Manager.Client.ViewModels;
using Fuel.Manager.Client.Views;

namespace Fuel.Manager.Client.Controllers
{
    public class CarController 
    {
        private CarView mView;
        private CarViewModel mViewModel;

        public CarController(CarView view, CarViewModel viewModel)
        {
            mView = view;
            mViewModel = viewModel;
            mView.DataContext = mViewModel;
        }


        public Car GetCar()
        {
            return mViewModel.SelectedCar;
        }

        public Car GetNewCar()
        {
            Car car = new Car();
            car.LicensePlate = mViewModel.LicensePlate;
            car.Vendor = mViewModel.Vendor;
            car.Model = mViewModel.Model;

            if(ValidateCarObject(car)) return null;

            return car;
        }

        public Car GetEditedCar()
        {
            Car car = GetCar();

            if (car == null) return car;

            Car editedCar = new Car();
            editedCar.Id = car.Id;
            editedCar.LicensePlate = mViewModel.LicensePlate;
            editedCar.Vendor = mViewModel.Vendor;
            editedCar.Model = mViewModel.Model;
            editedCar.Version = car.Version;
            return editedCar;
        }

        public void SetControllerData(List<Car> carList)
        {
            ObservableCollection<Car> o = mViewModel.Cars;
            o.Clear();

            foreach (Car car in carList)
            {
                o.Add(car);
            }
        }

        public void SetIsEnabled()
        {
            mViewModel.IsEnabled = !mViewModel.IsEnabled;
        }

        public bool ValidateCarObject(Car car)
        {
            mViewModel.ErrorMessage = "";
            if (string.IsNullOrEmpty(car.LicensePlate))
            {
                mViewModel.ErrorMessage = "Es muss ein Kennzeichen angegeben werden!";
                return true;
            }


            if (string.IsNullOrEmpty(car.Vendor))
            {
                mViewModel.ErrorMessage = "Es muss ein Hersteller angegeben werden!";
                return true;
            }

            if (string.IsNullOrEmpty(car.Model))
            {
                mViewModel.ErrorMessage = "Es muss ein Modell angegeben werden!";
                return true;
            }

            return false;
        }
    }
}
