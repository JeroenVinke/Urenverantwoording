using Urenverantwoording.ViewModels;

namespace Urenverantwoording.Models
{
    public class CurrentProjectChangedEvent
    {
        public readonly ProjectViewModel Project;

        public CurrentProjectChangedEvent(ProjectViewModel project)
        {
            Project = project;
        }
    }
}