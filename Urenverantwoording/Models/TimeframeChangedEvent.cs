using Urenverantwoording.ViewModels;

namespace Urenverantwoording.Models
{
    public class TimeframeChangedEvent
    {
        public TimeframeViewModel Timeframe { get; set; }

        public TimeframeChangedEvent(TimeframeViewModel viewmodel)
        {
            Timeframe = viewmodel;
        }
    }
}
