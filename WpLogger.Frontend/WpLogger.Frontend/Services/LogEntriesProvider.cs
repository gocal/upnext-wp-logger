using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Xml.Serialization;
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

        public LogEntriesProvider(WpLoggerService loggerService)
        {
            LogEntries = new ObservableCollection<LogEntry>();

            _loggerService = loggerService;
            _timer = new DispatcherTimer();

            _lastDate = DateTime.UtcNow.AddHours(-1);

            _timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
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
            var toDate = DateTime.UtcNow;
            var newItems = await _loggerService.GetLogs(_lastDate, toDate);

            if (newItems != null)
            {
                foreach (var item in newItems)
                {
                    LogEntries.Add(item);
                }

                if (NewDataAvaliableEvent != null)
                {
                    NewDataAvaliableEvent(newItems, _lastDate, toDate);
                }   
            }



            _lastDate = LogEntries.Last().TimeStamp;
        }


    

        #endregion
    }
}