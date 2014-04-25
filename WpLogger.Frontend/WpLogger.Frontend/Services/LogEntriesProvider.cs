using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
            var newItems = await _loggerService.GetLogs(_lastDate, toDate);

            /*

            var xmlSerializer = new XmlSerializer(typeof(TestObject));

            var testEntry = new TestObject()
            {
                text = "Entry " + DateTime.Now.Ticks,
                value = (int) (DateTime.Now.Ticks/2)
            };

            var content = "";

            using (var stringWriter = new Utf8StringWriter(CultureInfo.InvariantCulture))
            {
                xmlSerializer.Serialize(stringWriter, testEntry);
                content = stringWriter.ToString();
            }

            var newItems = new List<LogEntry>();

            newItems.Add(new LogEntry()
            {
                Content = content,
                LogLevel = "Debug",
                Tag = "Tag",
                TimeStamp = DateTime.Now
            });

            */

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

        public class TestObject
        {
            public int value;
            public string text;
        }


        public class Utf8StringWriter : StringWriter
        {
            #region Constructors and Destructors

            public Utf8StringWriter(IFormatProvider formatProvider)
                : base(formatProvider)
            {
            }

            #endregion

            #region Public Properties

            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }

            #endregion
        }

        #endregion
    }
}