using System.Collections.ObjectModel;

namespace Urenverantwoording.DomainLayer
{
    public class Project
    {
        public string Name { get; set; }
        public bool Finished { get; set; }
        public bool Collected { get; set; }
        public double ExpectedCost { get; set; }
        public double ExpectedHours { get; set; }
        public double DefinitiveCost { get; set; }
        public double HourlyWage { get; set; }

        public Project()
        {
            Timeframes = new ObservableCollection<Timeframe>();
        }

        public ObservableCollection<Timeframe> Timeframes { get; set; } 
    }
}
