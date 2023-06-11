
using Fuel.Manager.Client.ViewModels;
using Fuel.Manager.Client.Views;
using System;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Models;

namespace Fuel.Manager.Client.Controllers
{
    public class MainWindowController
    {
        private MainWindowViewModel mMainWindowViewModel;
        private MainWindow mView;
        private App mApplication;
        private Employee loggedInUser;

        public void Initialize()
        {
            var view = new MainWindow();
            mMainWindowViewModel = new MainWindowViewModel()
            {
                OpenCarCommand = new RelayCommand(ExecuteOpenCarCommand),
                OpenEmployeeCommand = new RelayCommand(ExecuteOpenEmployeeCommand)
            };
            view.DataContext = mMainWindowViewModel;

            view.ShowDialog();
        }

        private void ExecuteOpenEmployeeCommand(object obj)
        {
            var employeeViewController = new EmployeeController();
            mMainWindowViewModel.ActiveViewModel = employeeViewController.Initialize();
        }

        private void ExecuteOpenCarCommand(object obj)
        {
            var carViewController = new CarController();
            mMainWindowViewModel.ActiveViewModel = carViewController.Initialize();
        }
    }
}
