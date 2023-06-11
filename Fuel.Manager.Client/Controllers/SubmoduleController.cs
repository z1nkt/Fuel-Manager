using Fuel.Manager.Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel.Manager.Client.Controllers
{
    public abstract class SubmoduleController
    {
        public abstract ViewModelBase Initialize();
    }
}
