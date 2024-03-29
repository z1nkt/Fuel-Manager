﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.Models;

namespace Fuel.Manager.Client.ViewModels
{
    public class CarViewModel : ViewModelBase
    {
        public ObservableCollection<Car> Cars { get; set; }
        private Car _SelectedCar;
        public Car SelectedCar
        {
            get { return _SelectedCar; }
            set
            {
                if (_SelectedCar == value)
                {
                    return;
                }
                _SelectedCar = value;
                if (_SelectedCar != null)
                {
                    //updates the detail fields to the values of the selected item
                    LicensePlate = _SelectedCar.LicensePlate;
                    Vendor = _SelectedCar.Vendor;
                    Model = _SelectedCar.Model;
                }

                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private string _LicensePlate;
        public string LicensePlate
        {
            get { return _LicensePlate; }
            set
            {
                if (value == _LicensePlate)
                {
                    return;
                }
                _LicensePlate = value;
                OnPropertyChanged(nameof(LicensePlate));
            }
        }

        private string _Vendor;
        public string Vendor
        {
            get { return _Vendor; }
            set
            {
                if (value == _Vendor)
                {
                    return;
                }
                _Vendor = value;
                OnPropertyChanged(nameof(Vendor));
            }
        }

        private string _Model;
        public string Model
        {
            get { return _Model; }
            set
            {
                if (value == _Model)
                {
                    return;
                }
                _Model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private bool _IsEnabled;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (value == _IsEnabled)
                {
                    return;
                }

                _IsEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage == value) return;
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public CarViewModel()
        {
            Cars = new ObservableCollection<Car>();
        }

    }
}
