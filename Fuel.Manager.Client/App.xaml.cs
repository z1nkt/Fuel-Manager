using Fuel.Manager.Client.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;

namespace Fuel.Manager.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t =>
                t.IsClass && (t.Namespace.Contains("Controller") || t.Namespace.Contains("Framework") ||
                              t.Namespace.Contains("ViewModels") || t.Namespace.Contains("Views")));
            builder.RegisterInstance(this);

            this.Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var mainController = scope.Resolve<LoginController>();
                mainController.Initialize();
            }
        }
    }
}
