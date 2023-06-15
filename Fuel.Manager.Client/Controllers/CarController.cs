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
            Car c = new Car();
            c.LicensePlate = mViewModel.LicensePlate;
            c.Vendor = mViewModel.Vendor;
            c.Model = mViewModel.Model;

            return c;
        }

        public Car GetEditedCar()
        {
            Car c = GetCar();
            Car edited = new Car();
            edited.Id = c.Id;
            edited.LicensePlate = mViewModel.LicensePlate;
            edited.Vendor = mViewModel.Vendor;
            edited.Model = mViewModel.Model;
            edited.Version = c.Version;
            return edited;
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

        public bool ValidateInput()
        {
            mViewModel.ErrorMessage = "";
            if (string.IsNullOrEmpty(mViewModel.LicensePlate))
            {
                mViewModel.ErrorMessage = "Es muss ein Kennzeichen angegeben werden!";
                return true;
            }


            if (string.IsNullOrEmpty(mViewModel.Vendor))
            {
                mViewModel.ErrorMessage = "Es muss ein Hersteller angegeben werden!";
                return true;
            }

            if (string.IsNullOrEmpty(mViewModel.Model))
            {
                mViewModel.ErrorMessage = "Es muss ein Modell angegeben werden!";
                return true;
            }

            return false;
        }
    }
}
