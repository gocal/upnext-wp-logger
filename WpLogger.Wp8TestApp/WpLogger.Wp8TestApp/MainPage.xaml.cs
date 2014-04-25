using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
            _logger.Debug("button clicked");
        }
    }
}