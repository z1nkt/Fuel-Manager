using System.Windows;
using System.Windows.Controls;
using Fuel.Manager.Client.ViewModels;

namespace Fuel.Manager.Client.Views
{
    public class SubmoduleSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contentControl = (container as FrameworkElement);

            if (item is CarViewModel) return contentControl.FindResource("carViewTemplate") as DataTemplate;

            if (item is EmployeeViewModel) return contentControl.FindResource(("employeeViewTemplate")) as DataTemplate;

            return null;
        }
    }
}
