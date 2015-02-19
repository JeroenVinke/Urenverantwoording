using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Urenverantwoording.DataLayer;
using Urenverantwoording.Helpers;
using Urenverantwoording.Interfaces;
using Urenverantwoording.Properties;

namespace Urenverantwoording.ViewModels
{
    public class LauncherViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private readonly IDatastore _datastore;
        public IFileHelper FileHelper { get; private set; }


        public RecentFilesViewModel RecentFilesViewModel { get; private set; }


        public LauncherViewModel(IEventAggregator events, IDatastore datastore, IFileHelper fileHelper, RecentFilesViewModel recentFilesViewModel)
        {
            if (fileHelper == null)
            {
                throw new ArgumentNullException("fileHelper");
            }

            if (recentFilesViewModel == null)
            {
                throw new ArgumentNullException("recentFilesViewModel");
            }

            _events = events;
            _datastore = datastore;
            FileHelper = fileHelper;
            DisplayName = Resources.ApplicationName;


            RecentFilesViewModel = recentFilesViewModel;
            RecentFilesViewModel.PropertyChanged += RecentFileViewModelPropertyChanged;
        }

        private void RecentFileViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedFile")
            {
                NotifyOfPropertyChange(() => CanOpen);
            }
        }


        public void OpenFile(Models.File file)
        {
            _datastore.SetFilePath(file.Path);

            var windowManager = new WindowManager();
            var viewModel = IoC.Get<MainViewModel>();

            windowManager.ShowWindow(viewModel);

            var window = viewModel.GetView() as Window;
            Application.Current.MainWindow = window;
            TryClose();
        }

        public void Open()
        {
            if (!File.Exists(RecentFilesViewModel.SelectedFile.Path))
            {
                MessageBox.Show(Resources.DataFileNotFound);
                return;
            }

            OpenFile(RecentFilesViewModel.SelectedFile);
        }

        public bool CanOpen
        {
            get { return RecentFilesViewModel.SelectedFile != null; }
        }

        public void Close()
        {
            TryClose();
        }

        public void CreateFile()
        {
            var path = FileHelper.CreateFile();

            if (!string.IsNullOrEmpty(path))
            {
                RecentFilesViewModel.AddFile(path);
            }
        }


        public void ImportFile()
        {
            var path = FileHelper.OpenFile();

            if (!string.IsNullOrEmpty(path))
            {
                RecentFilesViewModel.AddFile(path);
            }
        }
    }
}
