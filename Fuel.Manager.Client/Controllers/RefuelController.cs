using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuel.Manager.Client.Models;
using Fuel.Manager.Client.Views;

namespace Fuel.Manager.Client.Controllers
{
    public class RefuelController
    {
        private RefuelView mView;
        private RefuelViewModel mViewModel;

        public RefuelController(RefuelView view, RefuelViewModel viewModel)
        {
            mView = view;
            mViewModel = viewModel;

            mView.DataContext = mViewModel;

            mViewModel.TodayCommand = new RelayCommand(ExecuteTodayCommand);
        }

        public void ExecuteTodayCommand(object o)
        {
            mViewModel.RefuelDate = DateTime.Now;
        }

        public Refuel GetRefuel()
        {
            return mViewModel.SelectedRefuel;
        }

        public Refuel GetNewRefuel()
        {
            Refuel refuel = new Refuel();
            refuel.Car = mViewModel.SelectedCar;
            refuel.Date = mViewModel.RefuelDate;
            refuel.Mileage = mViewModel.Mileage;
            refuel.Amount = mViewModel.Amount;
            refuel.Price = mViewModel.Price;

            return refuel;
        }

        public Refuel GetEditedRefuel()
        {
            Refuel refuel = GetRefuel();
            Refuel edited = new Refuel();
            edited.Id = refuel.Id;
            edited.Car = mViewModel.SelectedCar;
            edited.Date = mViewModel.RefuelDate;
            edited.Mileage = mViewModel.Mileage;
            edited.Amount = mViewModel.Amount;
            edited.Price = mViewModel.Price;
            edited.Version = refuel.Version;
            return edited;
        }

        public void SetControllerData(List<Refuel> refuelList, List<Car> carList)
        {
            ObservableCollection<Car> o = mViewModel.Cars;
            o.Clear();

            foreach (Car car in carList)
            {
                o.Add(car);
            }

            ObservableCollection<Refuel> r = mViewModel.Refuels;
            r.Clear();

            foreach (Refuel refuel in refuelList)
            {
                r.Add(refuel);
            }

            SetFirstRefuel();
        }

        public void SetFirstRefuel()
        {
            if (mViewModel.Refuels.Count > 0)
            {
                mViewModel.SelectedRefuel = mViewModel.Refuels.First();
            }
        }
    }
}
