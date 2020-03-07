using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
    public class BootStrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public BootStrapper()
        {
            Initialize();
            ConventionManager.AddElementConvention<System.Windows.Controls.PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        //Configure is ran once at the begining of the application
        protected override void Configure()
        {
            _container.Instance(_container);
            //A singleton creates one instance of the class for the life of the application.
            //These are kinda like a static class, we use them so we don't have to manage multiple instances of each.
            //These should be used as little as possible, and should only be used if you can't find a better way.
            _container
                //Iwindowmanager is an interface
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            //using reflection gettype (reflection is slow, use sparingly).  Only being used once in this app
            //This finds all the viewmodlels and put them in the _container adding interfaces to our viewmodel
            //This allows us to use a constructor to show different view models on the main page
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //This tells the system to start ShellViewModel on Startup in bootstrap
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);   
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
