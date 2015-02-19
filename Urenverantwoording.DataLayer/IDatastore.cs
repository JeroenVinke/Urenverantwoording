using System.Collections.ObjectModel;
using Urenverantwoording.DomainLayer;

namespace Urenverantwoording.DataLayer
{
    public interface IDatastore
    {
        ObservableCollection<Project> Projects { get; set; }
        void Save();
        void Load();
        bool HasChanged();
        void SetFilePath(string filePath);
    }
}