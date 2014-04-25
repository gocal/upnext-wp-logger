using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml.Serialization;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NLog;
using WpLogger.DataContract.Model;
using WpLogger.Wp8TestApp.Resources;
using WpLogger.Wp8TestApp.Services;

namespace WpLogger.Wp8TestApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string tag = "MainPage";

        private Logger _logger;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this._logger = LogManager.GetLogger(tag);
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _logger.Debug(TextBox.Text);
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<TestObject>));

            var list = new List<TestObject>();

            for (int i = 0; i < 10; i++)
            {
                var testEntry = new TestObject()
                {
                    text = "Entry " + i,
                    value = i
                };

                list.Add(testEntry);
   
            }

            var content = "";

            using (var stringWriter = new Utf8StringWriter(CultureInfo.InvariantCulture))
            {
                xmlSerializer.Serialize(stringWriter, list);
                content = stringWriter.ToString();
            }

            _logger.Debug(content);

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

        public class TestObject
        {
            public int value;
            public string text;
        }


    }
}