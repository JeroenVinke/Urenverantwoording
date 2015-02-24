using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using Urenverantwoording.DomainLayer;
using Urenverantwoording.Models;

namespace Urenverantwoording.ViewModels
{
    public class TimeframeListViewModel : PropertyChangedBase, IHandle<CurrentProjectChangedEvent>
    {
        private readonly IEventAggregator _events;
        private ProjectViewModel _project;


        private ObservableCollection<TimeframeViewModel> _timeframes;
        public ObservableCollection<TimeframeViewModel> Timeframes
        {
            get { return _timeframes; }
            private set
            {
                _timeframes = value;
                NotifyOfPropertyChange(() => Timeframes);
            }
        }

        
        


        private TimeframeViewModel _selectedTimeframe;
        public TimeframeViewModel SelectedTimeframe
        {
            get { return _selectedTimeframe; }
            set
            {
                _selectedTimeframe = value;

                PublishSelectedTimeframeChangedEvent();
                NotifyOfPropertyChange(() => SelectedTimeframe);
            }
        }



        private CollectionViewSource _viewSource;
        public CollectionViewSource ViewSource
        {
            get { return _viewSource; }
            set
            {
                _viewSource = value;

                NotifyOfPropertyChange(() => ViewSource);
            }
        }


        public TimeframeListViewModel(IEventAggregator events)
        {
            Timeframes = new ObservableCollection<TimeframeViewModel>();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = Timeframes;
            ViewSource.SortDescriptions.Add(new SortDescription("Start", ListSortDirection.Descending));
            ViewSource.View.Refresh();


            _events = events;
            _events.Subscribe(this);
        }



        public void PublishSelectedTimeframeChangedEvent()
        {
            _events.PublishOnUIThread(new SelectedTimeframeChangedEvent(SelectedTimeframe));
        }




        public void Create()
        {
            var timeframe = IoC.Get<TimeframeViewModel>();

            timeframe.Load(new Timeframe
            {
                Start = DateTime.Now,
                End = DateTime.Now,
                Project = _project.Project
            });

            Timeframes.Add(timeframe);


            SelectedTimeframe = timeframe;

            _project.Project.Timeframes.Add(timeframe.Timeframe);
            _project.NotifyOfPropertyChange(() => _project.TotalTime);
        }

        public bool CanCreate
        {
            get { return _project != null && !_project.Finished; }
        }

        public void Remove()
        {
            _project.Project.Timeframes.Remove(SelectedTimeframe.Timeframe);

            Timeframes.Remove(SelectedTimeframe);
            _project.NotifyOfPropertyChange(() => _project.TotalTime);

            SelectedTimeframe = Timeframes.LastOrDefault();
        }


        public bool CanRemove
        {
            get { return _project != null && !_project.Finished; }
        }

        public void Handle(CurrentProjectChangedEvent message)
        {
            _project = message.Project;

            Timeframes.Clear();

            if (_project != null)
            {
                foreach (var timeFrame in _project.Project.Timeframes)
                {
                    var vm = IoC.Get<TimeframeViewModel>();
                    vm.Load(timeFrame);

                    Timeframes.Add(vm);
                }


                _project.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == "Finished")
                    {
                        NotifyOfPropertyChange(() => CanRemove);
                        NotifyOfPropertyChange(() => CanCreate);
                    }
                };
            }

            NotifyOfPropertyChange(() => CanRemove);
            NotifyOfPropertyChange(() => CanCreate);
        }
    }
}
