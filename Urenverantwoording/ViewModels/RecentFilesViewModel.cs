using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using Urenverantwoording.Interfaces;
using Urenverantwoording.Models;

namespace Urenverantwoording.ViewModels
{
    public class RecentFilesViewModel : PropertyChangedBase
    {
        

        public IRecentFilesHelper RecentFilesHelper { get; private set; }


        public CollectionViewSource ViewSource
        {
            get { return _viewSource; }
            set
            {
                _viewSource = value;

                NotifyOfPropertyChange(() => ViewSource);
            }
        }


        private ObservableCollection<File> _files;
        public ObservableCollection<File> Files
        {
            get { return _files; }
            set
            {
                _files = value;

                NotifyOfPropertyChange(() => Files);
            }
        }



        private File _selectedFile;
        private CollectionViewSource _viewSource;

        public File SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;

                NotifyOfPropertyChange(() => SelectedFile);
            }
        }




        public delegate void FileOpenRequestHandler();
        public event FileOpenRequestHandler FileOpenRequest;



        public void RemoveFromList(File file)
        {
            RecentFilesHelper.RemoveFile(file);

            Refresh();
        }


        public RecentFilesViewModel(IRecentFilesHelper recentFilesHelper)
        {
            RecentFilesHelper = recentFilesHelper;

            Files = new ObservableCollection<File>();

            ViewSource = new CollectionViewSource();
            ViewSource.Source = Files;
            ViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            ViewSource.View.Refresh();

            Refresh();
        }


        public void Refresh()
        {
            Files.Clear();

            foreach (var file in RecentFilesHelper.GetFiles())
            {
                Files.Add(file);
            }
        }


        //public void Open()
        //{
        //    if (FileOpenRequest != null)
        //    {
        //        FileOpenRequest();
        //    }
        //}

        public void AddFile(string path)
        {
            RecentFilesHelper.AddFile(path);

            Refresh();
            
            SelectedFile = Files.FirstOrDefault(i => i.Path == path);
        }
    }
}
