using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using WpLogger.DataContract.Model;
using WpLogger.DataContract.Services;

namespace WpLogger.Frontend.Services
{
    public class LogEntriesProvider
    {
        #region Fields

        private readonly WpLoggerService _loggerService;

        private readonly DispatcherTimer _timer;

        private DateTime _lastDate;

        #endregion

        #region Constructors and Destructors

        public LogEntriesProvider()
        {
            LogEntries = new ObservableCollection<LogEntry>();

            _loggerService = new WpLoggerService();
            _timer = new DispatcherTimer();

            _lastDate = DateTime.Now.AddHours(-1);

            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Tick += TimerOnTick;
        }

        #endregion

        #region Delegates

        public delegate void NewDataAvaliableDelegate(List<LogEntry> items, DateTime from, DateTime to);

        #endregion

        #region Public Events

        public event NewDataAvaliableDelegate NewDataAvaliableEvent;

        #endregion

        #region Public Properties

        public ObservableCollection<LogEntry> LogEntries { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        #endregion

        #region Methods

        private async void TimerOnTick(object sender, EventArgs eventArgs)
        {
            var toDate = DateTime.Now;
            //var newItems = await _loggerService.GetLogs(_lastDate, toDate);

            var newItems = new List<LogEntry>();
            newItems.Add(new LogEntry()
            {
                Content = "Test",
                LogLevel = "Test",
                Tag = "Tag",
                TimeStamp = DateTime.Now
            });

            foreach (var item in newItems)
            {
                LogEntries.Add(item);
            }
        
            if (NewDataAvaliableEvent != null)
            {
                NewDataAvaliableEvent(newItems, _lastDate, toDate);
            }

            _lastDate = toDate;
        }

        #endregion
    }
}