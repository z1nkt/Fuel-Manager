using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.ViewModels;

namespace Fuel.Manager.Client.Controllers
{
    public class CarController : SubmoduleController
    {
        public override ViewModelBase Initialize()
        {
            return new CarViewModel();
        }
    }
}
