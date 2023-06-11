
using Fuel.Manager.Client.Framework;
using Fuel.Manager.Client.ViewModels;

namespace Fuel.Manager.Client.Controllers
{
    public class LoginController : SubmoduleController
    {
        public override ViewModelBase Initialize()
        {
            return new LoginViewModel();
        }
    }

}
