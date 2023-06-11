using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel.Manager.Client.Controllers
{
    public class EmployeeController : SubmoduleController
    {
        public override ViewModelBase Initialize()
        {
            return new EmployeeViewModel();
        }
    }
}
