using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Caliburn.Micro;
using Urenverantwoording.DataLayer;
using Urenverantwoording.Helpers;
using Urenverantwoording.Interfaces;
using Urenverantwoording.ViewModels;

namespace Urenverantwoording
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;



        protected override void Configure()
        {
            _container = new SimpleContainer();


            _container.PerRequest<IFileHelper, FileHelper>();
            _container.PerRequest<IRecentFilesHelper, RecentFilesHelper>();


            _container.PerRequest<LauncherViewModel>();
            _container.PerRequest<RecentFilesViewModel>();
            _container.PerRequest<TimeframeListViewModel>();
            _container.PerRequest<ProjectListViewModel>();
            _container.PerRequest<ProjectViewModel>();
            _container.PerRequest<MainViewModel>();
            _container.PerRequest<TimeframeViewModel>();




            _container.Instance<IEventAggregator>(new EventAggregator());
            _container.Instance<IWindowManager>(new WindowManager());



            _container.Singleton<IDatastore, Datastore>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }




        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LauncherViewModel>();
        }
    }
}
