using System.Collections.ObjectModel;

namespace Urenverantwoording.DomainLayer
{
    public class Project
    {
        public string Name { get; set; }

        public Project()
        {
            Timeframes = new ObservableCollection<Timeframe>();
        }

        public ObservableCollection<Timeframe> Timeframes { get; set; } 
    }
}
