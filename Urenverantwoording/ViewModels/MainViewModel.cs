using System;
using System.ComponentModel;
using System.Windows;
using Caliburn.Micro;
using Urenverantwoording.DataLayer;
using Urenverantwoording.Models;
using Urenverantwoording.Properties;

namespace Urenverantwoording.ViewModels
{
    public class MainViewModel : Screen, IHandle<SelectedTimeframeChangedEvent>, IHandle<EntityChangedEvent>
    {
        private readonly IEventAggregator _events;
        private readonly IDatastore _datastore;



        private TimeframeViewModel _timeframeViewModel;
        public TimeframeViewModel TimeframeViewModel
        {
            get { return _timeframeViewModel; }
            private set
            {

                _timeframeViewModel = value;



                NotifyOfPropertyChange(() => TimeframeViewModel);
                NotifyOfPropertyChange(() => TimeframeViewModelIsVisible);
            }
        }



        public bool TimeframeViewModelIsVisible
        {
            get { return TimeframeViewModel != null; }
        }


        private bool _canSave;
        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                _canSave = value;
                NotifyOfPropertyChange(() => CanSave);
            }
        }


        public ProjectListViewModel ProjectListViewModel { get; private set; }
        public TimeframeListViewModel TimeframeListViewModel { get; private set; }




        public MainViewModel(IEventAggregator events, 
            TimeframeListViewModel timeframeListViewModel,
            ProjectListViewModel projectListViewModel,
            IDatastore datastore)
        {
            _events = events;
            _events.Subscribe(this);


            _datastore = datastore;

            TimeframeViewModel = null;
            TimeframeListViewModel = timeframeListViewModel;
            ProjectListViewModel = projectListViewModel;

            DisplayName = Resources.ApplicationName;
        }







        public void Save()
        {
            try
            {
                _datastore.Save();

                CanSave = false;
            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show("Fout bij het opslaan." + e);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("Fout bij het opslaan." + e);
            }
        }





        public void Close(CancelEventArgs args)
        {
            if (_datastore.HasChanged())
            {
                if (
                    MessageBox.Show(Resources.UnsavedChangesCloseConfirmation, Resources.Confirmation,
                        MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    args.Cancel = true;
                }
            }
        }



        



        public void Handle(SelectedTimeframeChangedEvent message)
        {
            TimeframeViewModel = message.SelectedTimeframe;
        }

        public void Handle(EntityChangedEvent message)
        {
            CanSave = true;
        }
    }
}
