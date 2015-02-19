using System;
using System.ComponentModel;
using System.Text;
using Caliburn.Micro;
using Urenverantwoording.DomainLayer;
using Urenverantwoording.Interfaces;
using Urenverantwoording.Models;
using Urenverantwoording.Properties;

namespace Urenverantwoording.ViewModels
{
    public class ProjectViewModel : PropertyChangedBase, IDataErrorInfo, IWrapEntity<Project>, IHandle<TimeframeChangedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        public Project Project { get; set; }




        public string Name
        {
            get { return Project.Name; }
            set
            {
                if (value == Project.Name) return;
                Project.Name = value;

                NotifyOfPropertyChange(() => Name);
            }
        }


        public TimeSpan TotalTime
        {
            get
            {
                var totalTime = new TimeSpan(0, 0, 0);

                foreach (var timeFrame in Project.Timeframes)
                {
                    totalTime += (timeFrame.End - timeFrame.Start);
                }

                return totalTime;
            }
        }




        public ProjectViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);


            PropertyChanged += (o, a) =>
            {
                PublishEntityChangedEvent();
            };

            Project = new Project();
        }

        private void PublishEntityChangedEvent()
        {
            _eventAggregator.PublishOnUIThread(new EntityChangedEvent());
        }


        public void Load(Project project)
        {
            Project = project;
        }



        public void Handle(TimeframeChangedEvent message)
        {
            NotifyOfPropertyChange(() => TotalTime);
        }



        #region IDataErrorInfo 
        public string this[string columnName]
        {
            get
            {

                var errors = new StringBuilder();

                if (columnName == null || columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        errors.Append(Resources.Project_EmptyName);
                    }
                }

                return errors.Length == 0 ? "" : errors.ToString();
            }
        }

        public string Error
        {
            get { return this[null]; }
        }

        public bool IsValid
        {
            get { return string.IsNullOrEmpty(Error); }
        }

        #endregion



        
    }
}
