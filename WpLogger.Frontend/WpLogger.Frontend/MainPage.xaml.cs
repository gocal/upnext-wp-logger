using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WpLogger.DataContract.Model;
using WpLogger.Frontend.Services;

namespace WpLogger.Frontend
{
    public partial class MainPage : UserControl
    {
        private readonly LogEntriesProvider _logEntriesProvider;

        public MainPage()
        {
            InitializeComponent();

            _logEntriesProvider = new LogEntriesProvider();
            _logEntriesProvider.NewDataAvaliableEvent += LogEntriesProviderOnNewDataAvaliableEvent;

            DataGrid.ItemsSource = _logEntriesProvider.LogEntries;
        }

        private void LogEntriesProviderOnNewDataAvaliableEvent(List<LogEntry> items, DateTime @from, DateTime to)
        {
              
        }

        public void Start()
        {
            _logEntriesProvider.Start();     
        }

        public void Stop()
        {
            _logEntriesProvider.Stop();
        }

    }
}
