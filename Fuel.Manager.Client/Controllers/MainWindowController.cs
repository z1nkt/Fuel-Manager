
using Fuel.Manager.Client.ViewModels;
using Fuel.Manager.Client.Views;
using System;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Models;
using Autofac;
using System.Windows.Controls;
using System.Collections.Generic;
using Fuel.Manager.Client.Helper;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Fuel.Manager.Client.Controllers
{
    public class MainWindowController
    {
        private MainWindow mView;
        private MainWindowViewModel mViewModel;
        private App mApplication;

        private RefuelController RefuelController;
        private EmployeeController EmployeeController;
        private CarController CarController;

        private Employee loggedInUser;
        private List<Refuel> refuelsLoggedInUser = new List<Refuel>();
        private List<Car> carsLoggedInUser = new List<Car>();

        private List<Car> allCars = new List<Car>();
        private List<Refuel> allRefuels = new List<Refuel>();
        private List<Employee> allEmployees = new List<Employee>();
        private bool isEnabled = false;
        public MainWindowController(MainWindow mainWindow, MainWindowViewModel mainViewModel, App app)
        {
            mView = mainWindow;
            mApplication = app;
            mViewModel = mainViewModel;

            mView.DataContext = mViewModel;
            mApplication.MainWindow = mView;

            mViewModel.NewCommand = new RelayCommand(ExecuteNewCommand);
            mViewModel.EditCommand = new RelayCommand(ExecuteEditCommand);
            mViewModel.SaveCommand = new RelayCommand(ExecuteSaveCommand);
            mViewModel.DeleteCommand = new RelayCommand(ExecuteDeleteCommand);

            mainViewModel.Controller = this;
        }

        public void FrameChanged(Page p, string status)
        {
            switch (status)
            {
                case "Tanken":
                    ClearMessages();
                    RefuelController = new RefuelController(p as RefuelView, mApplication.Container.Resolve<RefuelViewModel>());
                    //get List of Cars and List of Refuels from Client
                    GetCarsAndRefuelsFromAllEmployees();
                    break;

                case "Fahrzeuge":
                    ClearMessages();
                    CarController = new CarController(p as CarView, mApplication.Container.Resolve<CarViewModel>());
                    //get List of Cars from Client
                    GetAllRefuels();
                    GetAllCars();
                    //set CarController data
                    CarController.SetControllerData(allCars);
                    break;

                case "Mitarbeiter":
                    ClearMessages();
                    EmployeeController = new EmployeeController(p as EmployeeView, mApplication.Container.Resolve<EmployeeViewModel>(), mApplication);
                    //get List of Employees from Client
                    GetAllEmployees();
                    //set EmployeeController data
                    EmployeeController.SetControllerData(allEmployees);
                    break;
            }
        }

        public async void ExecuteNewCommand(Object o)
        {
            ClearMessages();
            HttpClient client;
            string values;
            HttpResponseMessage response;
            string code;

            if (mViewModel is null)
            {
                return;
            }

            switch (mViewModel.SelectedMode)
            {
                case "Tanken":

                    

                    //save new Refuel object on Server
                    Refuel newRefuel = RefuelController.GetNewRefuel();
                    GetCarsAndRefuelsFromLoggedInEmployee();

                    if (RefuelController.ValidateInput()) break;

                    client = new HttpClient();
                    values = Mapper.RefuelToJson(newRefuel);

                    response = await client.PostAsync("http://localhost:5115/api/refuel/new", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Tanken konnte nicht gespeichert werden";
                    }

                    GetCarsAndRefuelsFromAllEmployees();
                    mViewModel.SuccessMessage = "Tanken wurde erfolgreich angelegt";
                    break;

                case "Fahrzeuge":
                    bool licenseOk = true;
                    Car newCar = CarController.GetNewCar();
                    GetAllCars();

                    if (CarController.ValidateInput()) break;
                    foreach (Car c in allCars)
                    {
                        if (newCar.LicensePlate.ToLower() == c.LicensePlate.ToLower())
                        {
                            mViewModel.ErrorMessage = "Kennzeichen existiert bereits, muss aber eindeutig sein";
                            licenseOk = false;
                        }
                    }

                    if (!licenseOk)
                    {
                        break;
                    }

                    //save new Car in Database
                    client = new HttpClient();
                    values = Mapper.CarToJson(newCar);

                    response = await client.PostAsync("http://localhost:5115/api/car/new", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Auto konnte nicht gespeichert werden";
                    }

                    GetAllCars();
                    mViewModel.SuccessMessage = "Fahrzeug wurde erfolgreich angelegt";
                    break;

                case "Mitarbeiter":
                    bool nameOk = true;
                    bool nomOk = true;
                    Employee newEmployee = EmployeeController.GetNewEmployee();
                    if (newEmployee == null)
                    {
                        mViewModel.ErrorMessage = "Es wurden keine Angaben eingegeben bitte erneut Versuchen";
                    }
                    GetAllEmployees();

                    if (EmployeeController.ValidateInput()) break;

                    //generate EmployeeNo
                    bool isGenerated = false;
                    int counter = 1;

                    while (!isGenerated)
                    {
                        if (newEmployee.EmployeeNo == null || newEmployee.EmployeeNo == "")
                        {
                            newEmployee.EmployeeNo = counter + "PCR";
                        }
                        else
                        {
                            isGenerated = true;
                        }

                        foreach (Employee e in allEmployees)
                        {
                            if (e.EmployeeNo == newEmployee.EmployeeNo)
                            {
                                isGenerated = false;
                                counter++;
                                newEmployee.EmployeeNo = "";
                                break;
                            }
                            else
                            {
                                isGenerated = true;
                            }
                        }
                    }


                    foreach (Employee c in allEmployees)
                    {
                        if (newEmployee.Username.ToLower() == c.Username.ToLower())
                        {
                            mViewModel.ErrorMessage = "Benutzername existiert bereits, muss aber eindeutig sein";
                            nameOk = false;
                        }

                        if (newEmployee.EmployeeNo.ToLower() == c.EmployeeNo.ToLower())
                        {
                            mViewModel.ErrorMessage = "Nummer existiert bereits, muss aber eindeutig sein";
                            nomOk = false;
                        }
                    }

                    if (!nameOk || !nomOk)
                    {
                        break;
                    }

                    //save new Employee in Database
                    client = new HttpClient();
                    var data = new Dictionary<string, string>
                    {
                        { "id", "0" },
                        { "username", newEmployee.Username },
                        { "password", EmployeeController.GetEmployeePassword() },
                        { "firstname", newEmployee.Firstname },
                        { "lastname", newEmployee.Lastname },
                        { "employeeno", newEmployee.EmployeeNo },
                        { "isadmin", newEmployee.IsAdmin.ToString() },
                        { "version", "1"}
                    };

                    //EmployeeController.ResetEmployeePassword();

                    values = JsonHelper.DictionaryToJson(data);
                    response = await client.PostAsync("http://localhost:5115/api/employee/new", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Nutzer konnte nicht gespeichert werden";
                    }

                    GetAllEmployees();
                    mViewModel.SuccessMessage = "Mitarbeiter wurde erfolgreich angelegt";
                    break;
            }
        }

        public void ExecuteEditCommand(Object o)
        {
            
            switch (mViewModel.SelectedMode)
            {
                case "Mitarbeiter":
                    EmployeeController.SetIsEnabled();
                    break;

                case "Tanken":
                    Console.WriteLine("Tanken");
                    RefuelController.SetIsEnabled();
                    break;

                case "Fahrzeuge":
                    CarController.SetIsEnabled();
                    break;


            }

        }

        public async void ExecuteSaveCommand(Object o)
        {
            mViewModel.ErrorMessage = "";
            HttpClient client;
            string values;
            HttpResponseMessage response;
            string code;

            switch (mViewModel.SelectedMode)
            {
                case "Tanken":
                    //save edited Refuel object on Server
                    Refuel refuel = RefuelController.GetEditedRefuel();
                    GetCarsAndRefuelsFromAllEmployees();

                    if (RefuelController.ValidateInput()) break;

                    client = new HttpClient();

                    values = Mapper.RefuelToJson(refuel);
                    response = await client.PostAsync("http://localhost:5115/api/refuel/edit", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Tanken konnte nicht gespeichert werden";
                    }

                    GetCarsAndRefuelsFromAllEmployees();
                    RefuelController.SetIsEnabled();
                    break;
                case "Fahrzeuge":
                    Car editedCar = CarController.GetEditedCar();
                    bool licenseOk = true;

                    GetAllCars();

                    if (CarController.ValidateInput()) break;

                    foreach (Car c in allCars)
                    {
                        if (editedCar.LicensePlate.ToLower() == c.LicensePlate.ToLower() && editedCar.Id != c.Id)
                        {
                            mViewModel.ErrorMessage = "Kennzeichen existiert bereits, muss aber eindeutig sein";
                            licenseOk = false;
                        }
                    }

                    if (!licenseOk)
                    {
                        break;
                    }



                    client = new HttpClient();

                    values = Mapper.CarToJson(editedCar);
                    response = await client.PostAsync("http://localhost:5115/api/car/edit", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Auto konnte nicht gespeichert werden";
                    }

                    GetAllCars();
                    CarController.SetIsEnabled();
                    break;
                case "Mitarbeiter":
                    Employee editedEmployee = EmployeeController.GetEditedEmployee();
                    bool ok = true;

                    GetAllEmployees();

                    if (EmployeeController.ValidateInput()) break;

                    foreach (Employee c in allEmployees)
                    {
                        if (editedEmployee.Username.ToLower() == c.Username.ToLower() && editedEmployee.Id != c.Id)
                        {
                            mViewModel.ErrorMessage = "Benutzername existiert bereits, muss aber eindeutig sein";
                            ok = false;
                        }

                        if (editedEmployee.EmployeeNo.ToLower() == c.EmployeeNo.ToLower() && editedEmployee.Id != c.Id)
                        {
                            mViewModel.ErrorMessage = "Nummer existiert bereits, muss aber eindeutig sein";
                            ok = false;
                        }
                    }

                    if (!ok)
                    {
                        break;
                    }

                    client = new HttpClient();

                    var data = new Dictionary<string, string>
                    {
                        { "id", editedEmployee.Id.ToString() },
                        { "username", editedEmployee.Username },
                        { "password", EmployeeController.GetEmployeePassword() },
                        { "firstname", editedEmployee.Firstname },
                        { "lastname", editedEmployee.Lastname },
                        { "employeeno", editedEmployee.EmployeeNo },
                        { "isadmin", editedEmployee.IsAdmin.ToString() },
                        { "version", editedEmployee.Version.ToString()}
                    };

                    EmployeeController.ResetEmployeePassword();

                    values = JsonHelper.DictionaryToJson(data);
                    response = await client.PostAsync("http://localhost:5115/api/employee/edit", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Nutzer konnte nicht gespeichert werden";
                    }

                    GetAllEmployees();
                    EmployeeController.SetIsEnabled();
                    break;
            }
        }

        public async void ExecuteDeleteCommand(Object o)
        {
            mViewModel.ErrorMessage = "";
            HttpClient client;
            string values;
            HttpResponseMessage response;
            string code;

            switch (mViewModel.SelectedMode)
            {
                case "Tanken":
                    Refuel deleted = RefuelController.GetRefuel();

                    client = new HttpClient();

                    values = Mapper.RefuelToJson(deleted);
                    response = await client.PostAsync("http://localhost:5115/api/refuel/delete", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Tanken konnte nicht gelöscht werden";
                    }

                    GetCarsAndRefuelsFromAllEmployees();
                    GetAllRefuels();
                    break;
                case "Fahrzeuge":
                    bool refuelExists = false;
                    Car deletedCar = CarController.GetCar();

                    //überprüfen, ob Refuel mit Car existiert

                    foreach (Refuel r in allRefuels)
                    {
                        if (r.Car.Id == deletedCar.Id)
                        {
                            mViewModel.ErrorMessage = "Es existiert ein Tankvorgang zu diesem Auto";
                            refuelExists = true;
                        }
                    }

                    if (refuelExists)
                    {
                        break;
                    }

                    client = new HttpClient();

                    values = Mapper.CarToJson(deletedCar);
                    response = await client.PostAsync("http://localhost:5115/api/car/delete", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Auto konnte nicht gelöscht werden";
                    }

                    GetAllCars();
                    CarController.SetControllerData(allCars);
                    break;
                case "Mitarbeiter":
                    //check whether there are cars connected to the employee
                    Employee deletedEmployee = EmployeeController.GetEmployee();

                    if (EmployeeController.GetCarsOfEmployee().Count() > 0)
                    {
                        mViewModel.ErrorMessage = "Es sind Autos mit dem Mitarbeiter verknüpft";
                        break;
                    }

                    client = new HttpClient();

                    var data = new Dictionary<string, string>
                    {
                        { "id", deletedEmployee.Id.ToString() },
                        { "username", deletedEmployee.Username },
                        { "password", EmployeeController.GetEmployeePassword() },
                        { "firstname", deletedEmployee.Firstname },
                        { "lastname", deletedEmployee.Lastname },
                        { "employeeno", deletedEmployee.EmployeeNo },
                        { "isadmin", deletedEmployee.IsAdmin.ToString() },
                        { "version", deletedEmployee.Version.ToString()}
                    };

                    //EmployeeController.ResetEmployeePassword();

                    values = JsonHelper.DictionaryToJson(data);
                    response = await client.PostAsync("http://localhost:5115/api/employee/delete", new StringContent(values, Encoding.UTF8, "application/json"));
                    code = response.StatusCode.ToString();
                    if (code != "OK")
                    {
                        mViewModel.ErrorMessage = "Benutzer konnte nicht gelöscht werden";
                    }

                    GetAllEmployees();
                    break;
            }
        }

        public async void GetCarsAndRefuelsFromLoggedInEmployee()
        {
            HttpClient client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                { "employeeid", loggedInUser.Id.ToString() }
            };

            var values = JsonHelper.DictionaryToJson(data);

            var response = await client.PostAsync("http://localhost:5115/api/employee/refuels",
                new StringContent(values, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            refuelsLoggedInUser = Mapper.JsonToRefuelList(responseString);

            refuelsLoggedInUser = refuelsLoggedInUser.OrderBy(r => r.Date).ToList();
        }

        public async void GetCarsAndRefuelsFromAllEmployees()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5115/api/cars");
            var responseString = await response.Content.ReadAsStringAsync();

            allCars = Mapper.JsonToCarList(responseString);

            client = new HttpClient();

            response = await client.GetAsync("http://localhost:5115/api/refuels");
            responseString = await response.Content.ReadAsStringAsync();

            allRefuels = Mapper.JsonToRefuelList(responseString);


            client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                { "employeeid", loggedInUser.Id.ToString() }
            };

            var values = JsonHelper.DictionaryToJson(data);

            response = await client.PostAsync("http://localhost:5115/api/employee/cars", new StringContent(values, Encoding.UTF8, "application/json"));
            responseString = await response.Content.ReadAsStringAsync();

            carsLoggedInUser = Mapper.JsonToCarList(responseString);

            RefuelController.SetControllerData(allRefuels, allCars, carsLoggedInUser);
        }


        public async void GetAllCars()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5115/api/cars");
            var responseString = await response.Content.ReadAsStringAsync();

            allCars = Mapper.JsonToCarList(responseString);
            CarController.SetControllerData(allCars);
        }

        public async void GetAllEmployees()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5115/api/employees");
            var responseString = await response.Content.ReadAsStringAsync();

            allEmployees = Mapper.JsonToEmployeeList(responseString);
            EmployeeController.SetControllerData(allEmployees);
        }

        public async void GetAllRefuels()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5115/api/refuels");
            var responseString = await response.Content.ReadAsStringAsync();

            allRefuels = Mapper.JsonToRefuelList(responseString);
        }

        public void Initialize()
        {
            LoginController loginController = mApplication.Container.Resolve<LoginController>();
            loggedInUser = loginController.Login();

            mViewModel.IsAdmin(loggedInUser.IsAdmin);

            mView.Show();
        }

        public void ClearMessages()
        {
            mViewModel.ErrorMessage = "";
            mViewModel.SuccessMessage = "";

        }


    }
}
