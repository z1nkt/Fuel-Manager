using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fuel.Manager.Client.Framework;

namespace Fuel.Manager.Client.ViewModels 
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginModel Model { get; set; }
        private string _userName;

        public string Username
        {
            get => _userName;
            set
            {
                if (_userName == value) return;
                _userName = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if( _password == value) return;
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if(_errorMessage == value) return;
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if(_isLoading == value) return;
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }


        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            Model = new LoginModel();
            ErrorMessage = "";
        }

    }
}
