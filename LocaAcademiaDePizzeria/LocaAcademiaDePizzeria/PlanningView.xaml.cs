﻿using System;
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


            //Centrado del mapa sobre la posición de la pizzería
            Geopoint pizzeriaPosition =  e.Parameter as Geopoint;
            mapaSoria.Center = pizzeriaPosition;
            mapaSoria.ZoomLevel = 15;


            //Creación de la imagen de la pizzería
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Pizzeria.png"));
            image.Width = 50;
            image.Height = 50;
            mapaSoria.Children.Add(image);
            MapControl.SetLocation(image, pizzeriaPosition);
            MapControl.SetNormalizedAnchorPoint(image, new Point(0.5, 0.5));


            //Creación de imagen de prueba (para calcular ruta)
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

            //Creación de la ruta pizzería -> imagen de prueba
            MapRouteFinderResult routeResult =
            await MapRouteFinder.GetDrivingRouteAsync(
            pizzeriaPosition,
            new Geopoint(soriaPosition2),
            MapRouteOptimization.Distance,
            MapRouteRestrictions.None);

            //Proceso de mostrar la ruta anterior en el mapa
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Inicializamos un MapRouteView
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Green;
                viewOfRoute.OutlineColor = Colors.Black;

                // Lo añadimos a la colección Routes del mapa
                mapaSoria.Routes.Add(viewOfRoute);

                // Encajamos la ruta en la pantalla
                /*await mapaSoria.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);*/
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
