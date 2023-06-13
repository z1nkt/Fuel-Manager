﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Models;
using Fuel.Manager.Client.ViewModels;
using Fuel.Manager.Client.Views;

namespace Fuel.Manager.Client.Controllers
{
    public class AddCarToEmployeeController
    {
        private AddCarToEmployeeWindow mView;
        private AddCarToEmployeeViewModel mViewModel;

        public AddCarToEmployeeController(AddCarToEmployeeWindow addView, AddCarToEmployeeViewModel addViewModel)
        {
            mView = addView;
            mViewModel = addViewModel;

            mView.DataContext = mViewModel;

            mViewModel.AddCommand = new RelayCommand(ExecuteAddCommand);
            mViewModel.CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }

        public void ExecuteAddCommand(object o)
        {
            mView.DialogResult = true;
        }

        public void ExecuteCancelCommand(object o)
        {
            mView.DialogResult = false;
        }

        public void SetControllerData(List<Car> cars)
        {
            foreach (Car car in cars)
            {
                mViewModel.Cars.Add(car);
            }
        }

        public Car GetSelectedCar()
        {
            mView.ShowDialog();

            if (mView.DialogResult == true)
            {
                return mViewModel.SelectedCar;
            }
            else
            {
                return null;
            }
        }
    }
}
