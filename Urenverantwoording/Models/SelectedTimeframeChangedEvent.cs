using Urenverantwoording.ViewModels;

namespace Urenverantwoording.Models
{
    public class SelectedTimeframeChangedEvent
    {
        public TimeframeViewModel SelectedTimeframe { get; private set; }

        public SelectedTimeframeChangedEvent(TimeframeViewModel selectedTimeframe)
        {
            SelectedTimeframe = selectedTimeframe;
        }
    }
}