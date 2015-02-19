using System;
using Newtonsoft.Json;

namespace Urenverantwoording.DomainLayer
{
    public class Timeframe
    {
        public string Activity { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }
    }
}
