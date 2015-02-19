using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Urenverantwoording.DomainLayer;

namespace Urenverantwoording.DataLayer
{
    public class Datastore : IDatastore
    {

        private string _filePath;


        public ObservableCollection<Project> Projects { get; set; }

        public Datastore()
        {
            Projects = new ObservableCollection<Project>();
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;

            Load();
        }
        

        public void Save()
        {
            
            var jsonContent = JsonConvert.SerializeObject(Projects);


            File.WriteAllText(_filePath, jsonContent);
        }

        public void Load()
        {
            var jsonContent = File.ReadAllText(_filePath);

            var deserialized = JsonConvert.DeserializeObject<Project[]>(jsonContent);

            if (deserialized == null)
            {
                deserialized = new Project[0];
            }

            Projects = new ObservableCollection<Project>(deserialized);


            foreach (var project in Projects)
            {
                foreach (var timeframe in project.Timeframes)
                {
                    timeframe.Project = project;
                }
            }
        }



        public bool HasChanged()
        {
            var oldJson = File.ReadAllText(_filePath);

            var newJson = JsonConvert.SerializeObject(Projects);

            return oldJson != newJson;
        }
    }
}
