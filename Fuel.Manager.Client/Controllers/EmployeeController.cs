﻿using Autofac;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Helper;
using Fuel.Manager.Client.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Fuel.Manager.Client.ViewModels;
using Fuel.Manager.Client.Views;

namespace Fuel.Manager.Client.Controllers
{
    public class EmployeeController 
    {
        private EmployeeView mView;
        private EmployeeViewModel mViewModel;

        private LinkCarToEmployeeController _mLinkCarToEmployeeController;
        private App mApplication;

        public EmployeeController(EmployeeView view, EmployeeViewModel viewModel, App app)
        {
            mView = view;
            mViewModel = viewModel;

            mView.DataContext = mViewModel;

            mViewModel.AddCarCommand = new RelayCommand(ExecuteAddCarCommand);
            mViewModel.RemoveCarCommand = new RelayCommand(ExecuteRemoveCarCommand);
            mViewModel.EmployeeController = this;
            mApplication = app;
        }

        public async void GetCarsFromEmployeeID(int employeeId)
        {
            HttpClient client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                { "employeeid", employeeId.ToString() }
            };

            var values = JsonHelper.DictionaryToJson(data);

            var response = await client.PostAsync("http://localhost:5115/api/employee/cars", new StringContent(values, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            List<Car> cars = Mapper.JsonToCarList(responseString);

            mViewModel.Cars.Clear();
            foreach (var car in cars)
            {
                mViewModel.Cars.Add(car);
            }
        }

        public Employee GetEmployee()
        {
            return mViewModel.SelectedEmployee;
        }

        public Employee GetNewEmployee()
        {
            Employee e = new Employee();
            e.EmployeeNo = mViewModel.EmployeeNo;
            e.Username = mViewModel.Username;
            e.Firstname = mViewModel.Firstname;
            e.Lastname = mViewModel.Lastname;
            e.IsAdmin = mViewModel.IsAdmin;
            return e;
        }

        public Employee GetEditedEmployee()
        {
            Employee e = GetEmployee();
            Employee edited = new Employee();
            edited.Id = e.Id;
            edited.EmployeeNo = mViewModel.EmployeeNo;
            edited.Username = mViewModel.Username;
            edited.Firstname = mViewModel.Firstname;
            edited.Lastname = mViewModel.Lastname;
            edited.IsAdmin = mViewModel.IsAdmin;
            edited.Version = e.Version;
            return edited;
        }

        public List<Car> GetCarsOfEmployee()
        {
            List<Car> cars = new List<Car>();
            var viewCars = mViewModel.Cars;

            foreach (var car in viewCars)
            {
                cars.Add(car);
            }

            return cars;
        }

        public string GetEmployeePassword()
        {
            return mViewModel.Password;
        }

        public void ResetEmployeePassword()
        {
            mView.ResetPasswordBox();
        }

        public void SetControllerData(List<Employee> employeeList)
        {
            ObservableCollection<Employee> e = mViewModel.Employees;
            e.Clear();

            foreach (Employee employee in employeeList)
            {
                e.Add(employee);
            }

        }

        public void SetIsEnabled()
        {
            mViewModel.IsEnabled = !mViewModel.IsEnabled;
        }

         public async void ExecuteAddCarCommand(object o)
         {
             _mLinkCarToEmployeeController = mApplication.Container.Resolve<LinkCarToEmployeeController>();

             //add all cars to Controller
             GetAllCarsForSelection();

             Car car = _mLinkCarToEmployeeController.GetSelectedCar();


             if (car != null)//without cancel button is not working for obvious reasons :)
             {
                 HttpClient client = new HttpClient();

                 var data = new Dictionary<string, string>
             {
                 { "employeeid", mViewModel.SelectedEmployee.Id.ToString() },
                 { "carid", car.Id.ToString() }
             };

                 var values = JsonHelper.DictionaryToJson(data);

                 await client.PostAsync("http://localhost:5115/api/employee/car/add", new StringContent(values, Encoding.UTF8, "application/json"));

                 if (!(car == null))
                 {
                     mViewModel.Cars.Add(car);
                 }
             }
         }

        public async void ExecuteRemoveCarCommand(object o)
        {
            if (!(mViewModel.SelectedCar == null))
            {
                HttpClient client = new HttpClient();

                var data = new Dictionary<string, string>
            {
                { "employeeid", mViewModel.SelectedEmployee.Id.ToString() },
                { "carid", mViewModel.SelectedCar.Id.ToString() }
            };

                var values = JsonHelper.DictionaryToJson(data);

                await client.PostAsync("http://localhost:5115/api/employee/car/delete", new StringContent(values, Encoding.UTF8, "application/json"));

                mViewModel.Cars.Remove(mViewModel.SelectedCar);
            }
        }

        public async void GetAllCarsForSelection()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5115/api/cars");
            var responseString = await response.Content.ReadAsStringAsync();

            List<Car> allCars = Mapper.JsonToCarList(responseString);

            //remove already added cars
            List<Car> addedCars = mViewModel.Cars.ToList();
            List<Car> toBeRemoved = new List<Car>();

            foreach (Car all in allCars)
            {
                foreach (Car added in addedCars)
                {
                    if (all.Id == added.Id)
                    {
                        toBeRemoved.Add(all);
                    }
                }
            }

            foreach (Car remove in toBeRemoved)
            {
                allCars.Remove(remove);
            }

            _mLinkCarToEmployeeController.SetControllerData(allCars);
        }


        public bool ValidateInput()
        {
            mViewModel.ErrorMessage = "";
            if (string.IsNullOrEmpty(mViewModel.Username))
            {
                mViewModel.ErrorMessage = "Es muss ein Benutzername angegeben werden!";
                return true;
            }


            if (string.IsNullOrEmpty(mViewModel.Lastname))
            {
                mViewModel.ErrorMessage = "Es muss ein Nachname angegeben werden!";
                return true;
            }

            if (string.IsNullOrEmpty(mViewModel.Password))
            {
                mViewModel.ErrorMessage = "Es muss ein Password angegeben werden!";
                return true;
            }

            

            if (mViewModel.Username.Length > 50 || mViewModel.Firstname.Length > 50 || mViewModel.Lastname.Length > 50)
            {
                mViewModel.ErrorMessage = "Angabe darf nicht länger als 50 Zeichen sein";
                return true;
            }

            if (mViewModel.Password.Length > 60)
            {
                mViewModel.ErrorMessage = "Passwort darf nicht länger als 60 Zeichen sein";
                return true;
            }

            if (mViewModel.Username.Length > 10)
            {
                mViewModel.ErrorMessage = "Benutzername darf nicht länger als 10 Zeichen sein";
                return true;
            }

            return false;
        }


    }
}

