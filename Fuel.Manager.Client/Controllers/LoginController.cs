
using System;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Helper;
using Fuel.Manager.Client.Models;
using Fuel.Manager.Client.ViewModels;
using Fuel.Manager.Client.Views;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Fuel.Manager.Client.Controllers
{
    public class LoginController 
    {

        private LoginControl mView;
        private LoginViewModel mViewModel;

        private Employee employee;

        public LoginController(LoginControl loginControl, LoginViewModel loginViewModel)
        {
            mView = new LoginControl();
            mViewModel = new LoginViewModel();

            mView.DataContext = mViewModel;
            mViewModel.LoginCommand = new RelayCommand(ExecuteLoginCommand);
            mView.ShowDialog();

        }

        public async void ExecuteLoginCommand(object obj)
        {
            Console.WriteLine("Hallo");
            HttpClient client = new HttpClient();

            var data = new Dictionary<string, string>
            {
                { "username", mViewModel.Username.ToLower() },
                { "password", mViewModel.Password.ToLower() },
            };

            var values = JsonHelper.DictionaryToJson(data);
            var response = await client.PostAsync("http://localhost:3000/api/login", new StringContent(values, Encoding.UTF8, "application/json"));
            string code = response.StatusCode.ToString();
            var responseString = await response.Content.ReadAsStringAsync();

            if (code == "OK")
            {
                //parse to employee
                employee = Mapper.JsonToEmployee(responseString);

                mView.DialogResult = true;
            }
            else
            {
                mViewModel.ErrorMessage = "Benutzername oder Passwort falsch";
            }
        }

        public Employee Login()
        {
            mView.ShowDialog();
            while (mView.DialogResult == false)
            {

            }

            return employee;
        }
    }
}
