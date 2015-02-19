using System;
using Caliburn.Micro;
using Urenverantwoording.DomainLayer;
using Urenverantwoording.Interfaces;
using Urenverantwoording.Models;

namespace Urenverantwoording.ViewModels
{
    public class TimeframeViewModel : PropertyChangedBase, IWrapEntity<Timeframe>
    {
        private readonly IEventAggregator _eventAggregator;
        public Timeframe Timeframe { get; set; }


        public string Activity
        {
            get { return Timeframe.Activity; }
            set
            {
                if (value == Timeframe.Activity) return;
                Timeframe.Activity = value;

                NotifyOfPropertyChange(() => Activity);
            }
        }


        public string Description
        {
            get { return Timeframe.Description; }
            set
            {
                if (value == Timeframe.Description) return;
                Timeframe.Description = value;

                NotifyOfPropertyChange(() => Description);
            }
        }


        public DateTime Date
        {
            get { return Start.Date; }
            set
            {
                Start = value + Start.TimeOfDay;
                End = value + End.TimeOfDay;
            }
        }
        public DateTime Start
        {
            get { return Timeframe.Start; }
            set
            {
                if (value.Equals(Timeframe.Start)) return;

                Timeframe.Start = value;

                NotifyOfPropertyChange(() => Start);
                NotifyOfPropertyChange(() => Total);
            }
        }
        public DateTime End
        {
            get { return Timeframe.End; }
            set
            {
                if (value.Equals(Timeframe.End)) return;

                Timeframe.End = value;

                NotifyOfPropertyChange(() => End);
                NotifyOfPropertyChange(() => Total);
            }
        }
        public TimeSpan Total
        {
            get { return Timeframe.End - Timeframe.Start; }
        }



 





        public void StartNow()
        {
            Start = DateTime.Now;
        }

        public void EndNow()
        {
            End = DateTime.Now;
        }


        public TimeframeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            PropertyChanged += (o, a) =>
            {
                PublishEntityChangedEvent();
                PublishTimeframeChangedEvent();
            };
        }

        private void PublishEntityChangedEvent()
        {
            _eventAggregator.PublishOnUIThread(new EntityChangedEvent());
        }

        private void PublishTimeframeChangedEvent()
        {
            _eventAggregator.PublishOnUIThread(new TimeframeChangedEvent(this));
        }

        public void Load(Timeframe entity)
        {
            Timeframe = entity;
        }
    }
}
