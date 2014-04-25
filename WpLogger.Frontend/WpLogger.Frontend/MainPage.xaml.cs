using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SharpGIS.SyntaxHighlighting;
using WpLogger.DataContract.Model;
using WpLogger.DataContract.Services;
using WpLogger.Frontend.Services;

namespace WpLogger.Frontend
{
    public partial class MainPage : UserControl
    {
        private readonly LogEntriesProvider _logEntriesProvider;

        private WpLoggerService loggerService;

        public MainPage()
        {
            InitializeComponent();

            loggerService = new WpLoggerService();

            _logEntriesProvider = new LogEntriesProvider(loggerService);
            _logEntriesProvider.NewDataAvaliableEvent += LogEntriesProviderOnNewDataAvaliableEvent;

            DataGrid.ItemsSource = _logEntriesProvider.LogEntries;


        }

        private void LogEntriesProviderOnNewDataAvaliableEvent(List<LogEntry> items, DateTime @from, DateTime to)
        {
            // scroll to bottom
            DataGridScroll.Measure(DataGridScroll.RenderSize);
            DataGridScroll.ScrollToVerticalOffset(DataGridScroll.ScrollableHeight);      
        }

        public void Start()
        {
            loadAppsAndDevices();
            _logEntriesProvider.Start();
      

        }

        public void Stop()
        {
            _logEntriesProvider.Stop();
        }

        private async void loadAppsAndDevices()
        {
            devices = new ObservableCollection<string>(await loggerService.GetDevices());

            Devices.ItemsSource = devices;

            apps = new ObservableCollection<string>(await loggerService.GetApps(devices[0]));
            Apps.ItemsSource = apps;
            loggerService.setApp(apps[0], devices[0]);
            
        }

        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var entry = e.AddedItems[0] as LogEntry;

                CodeHighlighter.SourceCode = entry.Content;
            }
        }

        private string appId;
        private string deviceId;
        private ObservableCollection<String> apps;
        private ObservableCollection<String> devices; 

        private async void Devices_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var entry = e.AddedItems[0] as string;
                deviceId = entry;
                apps.Clear();

                var updatedApps = await loggerService.GetApps(deviceId);

                foreach (var a in updatedApps)
                {
                    apps.Add(a);
                }

                if (apps.Count > 0)
                {
                    appId = apps[0];
                    loggerService.setApp(appId, deviceId);
                }
            }   
        }

        private void Apps_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var entry = e.AddedItems[0] as string;
                appId = entry;

                if (!String.IsNullOrWhiteSpace(appId) && !String.IsNullOrWhiteSpace(deviceId))
                {
                    _logEntriesProvider.LogEntries.Clear();
                    loggerService.setApp(appId, deviceId);
                }

            }  
        }
    }
}
