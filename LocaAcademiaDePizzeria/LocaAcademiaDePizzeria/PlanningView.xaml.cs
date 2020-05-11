using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using Windows.UI;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace LocaAcademiaDePizzeria
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    /// 
    public sealed partial class PlanningView : Page
    {
        public ObservableCollection<DriverViewModel> DriverList { get; } = new ObservableCollection<DriverViewModel>();
        public ObservableCollection<AbilityViewModel> AbilityList { get; } = new ObservableCollection<AbilityViewModel>(); 
        public PlanningView()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Carga la lista de ModelView a partir de la lista de Modelo
            if (DriverList != null)
                foreach (Driver driver in DriverModel.GetAllDrivers())
                {
                    DriverViewModel VMitem = new DriverViewModel(driver);
                    DriverList.Add(VMitem);
                }
            // Carga la lista de ModelView a partir de la lista de Modelo
            if (AbilityList != null)
                foreach (Ability ability in AbilityModel.GetAllAbilities())
                {
                    AbilityViewModel VMitem = new AbilityViewModel(ability);
                    AbilityList.Add(VMitem);
                }
            CloseAbilties();
            CloseDrivers();

            BasicGeoposition soriaPosition;
            soriaPosition.Latitude = 41.764609;
            soriaPosition.Longitude = -2.472443;
            soriaPosition.Altitude = 2000;

            mapaSoria.Center = new Geopoint(soriaPosition);
            mapaSoria.ZoomLevel = 15;

            Windows.UI.Xaml.Controls.Image image = new Image();
            image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icon.png"));
            image.Width = 50;
            image.Height = 50;
            mapaSoria.Children.Add(image);
            MapControl.SetLocation(image, new Geopoint(soriaPosition));
            MapControl.SetNormalizedAnchorPoint(image, new Point(0.5, 0.5));


            BasicGeoposition soriaPosition2;
            soriaPosition2.Latitude = 41.768670;
            soriaPosition2.Longitude = -2.482230;
            soriaPosition2.Altitude = 2000;
            Windows.UI.Xaml.Controls.Image image2 = new Image();
            image2.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icon.png"));
            image2.Width = 50;
            image2.Height = 50;
            mapaSoria.Children.Add(image2);
            MapControl.SetLocation(image2, new Geopoint(soriaPosition2));
            MapControl.SetNormalizedAnchorPoint(image2, new Point(0.5, 0.5));

            MapRouteFinderResult routeResult =
            await MapRouteFinder.GetDrivingRouteAsync(
            new Geopoint(soriaPosition),
            new Geopoint(soriaPosition2),
            MapRouteOptimization.Distance,
            MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.BlueViolet;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapaSoria.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapaSoria.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }

            base.OnNavigatedTo(e);
        }

        private void OpenAbilties()
        {
            Button_OpenAbilitiesGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Grid_expandedAbility.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
        private void CloseAbilties()
        {
            Button_OpenAbilitiesGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Grid_expandedAbility.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void OpenDrivers()
        {
            Grid_expandedDrivers.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Grid_collapsedDrivers.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Grid_DriverInfo.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
        private void CloseDrivers()
        {
            Grid_expandedDrivers.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Grid_DriverInfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Grid_collapsedDrivers.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void Button_OpenAbilitiesGrid_Click(object sender, RoutedEventArgs e)
        {
            OpenAbilties();
        }

        private void Button_CloseAbilites_Click(object sender, RoutedEventArgs e)
        {
            CloseAbilties();
        }

        private void Button_CloseDrivers_Click(object sender, RoutedEventArgs e)
        {
            CloseDrivers();
        }

        private void OpenDriversButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDrivers();
        }

        private void PizzaTime_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManualView));
        }
    }
}
