using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Fuel.Manager.Client.Controllers;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Views;

namespace Fuel.Manager.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowController Controller { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public Page SelectedPage { get; set; }
        public ObservableCollection<String> Mode { get; set; }

        private String _SelectedMode;
        public String SelectedMode
        {
            get { return _SelectedMode; }
            set
            {
                if (value == _SelectedMode)
                {
                    return;
                }
                _SelectedMode = value;

                switch (value)
                {
                    case "Tanken":
                        SelectedPage = new RefuelView();
                        OnPropertyChanged("SelectedPage");
                        Controller.FrameChanged(SelectedPage, "Tanken");
                        break;
                    case "Fahrzeuge":
                        SelectedPage = new CarView();
                        OnPropertyChanged("SelectedPage");
                        Controller.FrameChanged(SelectedPage, "Fahrzeuge");
                        break;
                    case "Mitarbeiter":
                        SelectedPage = new EmployeeView();
                        OnPropertyChanged("SelectedPage");
                        Controller.FrameChanged(SelectedPage, "Mitarbeiter");
                        break;
                }
                OnPropertyChanged("Page");
                OnPropertyChanged("SelectedPage");
                OnPropertyChanged("SelectedMode");

            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                if (value == _ErrorMessage)
                    return;

                _ErrorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private string _SuccessMessage;
        public string SuccessMessage
        {
            get { return _SuccessMessage; }
            set
            {
                if (value == _SuccessMessage)
                    return;

                _SuccessMessage = value;
                OnPropertyChanged(nameof(SuccessMessage));
            }
        }

        public MainWindowViewModel()
        {
            Mode = new ObservableCollection<String>();
            Mode.Add("Tanken");
            Mode.Add("Fahrzeuge");
            Mode.Add("Mitarbeiter");
        }

        public void IsAdmin(bool isAdmin)
        {
            if (!isAdmin)
            {
                Mode.Remove("Fahrzeuge");
                Mode.Remove("Mitarbeiter");
            }
        }



    }
}
