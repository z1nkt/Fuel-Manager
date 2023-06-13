using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fuel.Manager.Client.Views
{
    /// <summary>
    /// Interaktionslogik für EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Page
    {
        public EmployeeView()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword; }
        }

        public string GetPassword()
        {
            PasswordBox p = this.FindName("keinMVVMC") as PasswordBox;
            return p.Password;
        }

        public void ResetPasswordBox()
        {
            PasswordBox p = this.FindName("keinMVVMC") as PasswordBox;
            p.Password = string.Empty;
        }

    }
}
