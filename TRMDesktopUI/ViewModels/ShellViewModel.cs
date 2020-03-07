using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel :Conductor<Object>
    {
        private LoginViewModel _loginvm;
        public ShellViewModel(LoginViewModel loginvm)
        {
            _loginvm = loginvm;
            ActivateItem(_loginvm);
        }
    }
}
