using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Urenverantwoording.DataLayer;
using Urenverantwoording.DomainLayer;
using Urenverantwoording.Models;
using Urenverantwoording.Properties;

namespace Urenverantwoording.ViewModels
{
    public class ProjectListViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;
        private IDatastore _datastore;

        private ObservableCollection<ProjectViewModel> _projects;
        public ObservableCollection<ProjectViewModel> Projects
        {
            get { return _projects; }
            set
            {
                if (Equals(value, _projects)) return;
                _projects = value;

                NotifyOfPropertyChange(() => Projects);
            }
        }


        private ProjectViewModel _selectedProject;
        public ProjectViewModel SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (Equals(value, _selectedProject)) return;
                _selectedProject = value;

                PublishCurrentProjectChangedEvent();

                NotifyOfPropertyChange(() => SelectedProject);
            }
        }

        private void PublishCurrentProjectChangedEvent()
        {
            _events.PublishOnUIThread(new CurrentProjectChangedEvent(SelectedProject));
        }


        public ProjectListViewModel(IDatastore store, IEventAggregator events)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }

            _events = events;
            _events.Subscribe(this);


            _datastore = store;



            Projects = new ObservableCollection<ProjectViewModel>();
            foreach (var project in _datastore.Projects)
            {
                var viewModel = IoC.Get<ProjectViewModel>();
                viewModel.Load(project);
                Projects.Add(viewModel);
            }
            


            if (Projects.Any())
            {
                SelectedProject = Projects.First();
            }


            _datastore.Projects.CollectionChanged += Projects_CollectionChanged;
        }






        public void CreateProject()
        {
            _datastore.Projects.Add(new Project());
        }



        public void RemoveProject()
        {
            if (MessageBox.Show(Resources.RemoveProjectConfirmation, Resources.Confirmation, MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;

            _datastore.Projects.Remove(SelectedProject.Project);
        }

        public bool CanRemoveProject()
        {
            return Projects.Any() && SelectedProject != null;
        }





        void Projects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null && args.NewItems.Count > 0)
            {
                foreach (var project in args.NewItems.Cast<Project>())
                {

                    var vm = IoC.Get<ProjectViewModel>();
                    vm.Load(project);

                    Projects.Add(vm);

                    SelectedProject = vm;
                }
            }

            if (args.OldItems != null && args.OldItems.Count > 0)
            {
                foreach (var project in args.OldItems.Cast<Project>())
                {
                    Projects.Remove(Projects.FirstOrDefault(i => i.Name == project.Name));
                }


                SelectedProject = Projects.FirstOrDefault();
            }
        }
    }
}
