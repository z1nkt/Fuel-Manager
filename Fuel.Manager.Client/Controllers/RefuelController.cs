﻿using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuel.Manager.Client.Models;
using Fuel.Manager.Client.Views;
using Fuel.Manager.Client.Helper;
using System.Net.Http;

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

            if(ValidateRefuelObject(refuel)) return null;
            else return refuel;




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

        public void SetControllerData(List<Refuel> refuelList, List<Car> carList, List<Car> employeeCarList)
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

            ObservableCollection<Car> ec = mViewModel.EmployeeCars;
            ec.Clear();
            foreach (Car car in employeeCarList)
            {
                ec.Add(car);
            }
                

        }

        public void SetIsEnabled()
        {
            mViewModel.IsEnabled = !mViewModel.IsEnabled;
        }

        public bool ValidateRefuelObject(Refuel refuel)
        {
            mViewModel.ErrorMessage = "";

            if (refuel.Car == null )
            {
                mViewModel.ErrorMessage = "Es muss ein Fahrzeug ausgewählt sein";
                return true;

            }
            return false;
        }

        public bool ValidateInput()
        {
            mViewModel.ErrorMessage = "";
            /*if (string.IsNullOrEmpty(mViewModel.SelectedCar.LicensePlate))
            {
                mViewModel.ErrorMessage = "Es muss ein Fahrzeug ausgewählt sein";
                return true;
            }*/

            if (decimal.IsNegative(mViewModel.Price))
            {
                mViewModel.ErrorMessage = "Preis darf nicht negativ sein!";
                return true;
            }

            if (decimal.IsNegative(mViewModel.Amount))
            {
                mViewModel.ErrorMessage = "Liter können nicht negativ sein!";
                return true;
            }

            if (int.IsNegative(mViewModel.Mileage))
            {
                mViewModel.ErrorMessage = "Kilometerstand kann nicht negativ sein!";
                return true;
            }

            return false;
        }
    }
}
