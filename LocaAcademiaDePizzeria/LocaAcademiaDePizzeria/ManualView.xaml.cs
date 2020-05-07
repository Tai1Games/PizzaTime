using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Diagnostics;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace LocaAcademiaDePizzeria
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ManualView : Page, INotifyPropertyChanged
    {

        public ObservableCollection<Ability> abilities { get; } = new ObservableCollection<Ability>();

        public ManualView()
        {
            this.InitializeComponent();

            //mapa
            BasicGeoposition soriaPosition;
            soriaPosition.Latitude = 41.764609;
            soriaPosition.Longitude = -2.472443;
            soriaPosition.Altitude = 2000;
            mapaSoria.Center = new Geopoint(soriaPosition);
            mapaSoria.ZoomLevel = 15;

            //timer
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler<object>(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, timerSpeed);
            dispatcherTimer.Start();
            DateTime d = new DateTime();
            d.Subtract(d);
            dateTimer = d.AddMinutes(1170);

            // Carga la lista de ModelView a partir de la lista de Modelo
            if (abilities != null)
                foreach (Ability ability in AbilityModel.GetAllAbilities())
                {
                    abilities.Add(ability);
                }
        }

        public PointerPoint firstPoint = null;
        public int maxJoystickDistance = 60;
        public int timerSpeed = 10;
        public DateTime dateTimer;

        private void StreetCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            firstPoint = e.GetCurrentPoint(StreetCanvas);
            Canvas.SetLeft(JoystickBorder, firstPoint.Position.X - JoystickBorder.ActualHeight/2);
            Canvas.SetTop(JoystickBorder, firstPoint.Position.Y - JoystickBorder.ActualHeight/2);
            Canvas.SetLeft(Joystick, firstPoint.Position.X - Joystick.ActualWidth/2);
            Canvas.SetTop(Joystick, firstPoint.Position.Y - Joystick.ActualHeight/2);
            JoystickBorder.Visibility = Visibility.Visible;
            Joystick.Visibility = Visibility.Visible;
        }

        private void StreetCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (firstPoint != null)
            {
                PointerPoint newPoint = e.GetCurrentPoint(StreetCanvas);
                double xDiff = newPoint.Position.X - firstPoint.Position.X;
                double yDiff = newPoint.Position.Y - firstPoint.Position.Y;
                if (Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2) < Math.Pow(maxJoystickDistance, 2))
                {
                    Canvas.SetLeft(Joystick, newPoint.Position.X - Joystick.ActualWidth / 2);
                    Canvas.SetTop(Joystick, newPoint.Position.Y - Joystick.ActualHeight / 2);
                }
                else
                {
                    double angle = Math.Atan2(yDiff, xDiff);
                    Canvas.SetLeft(Joystick, firstPoint.Position.X + Math.Cos(angle) * maxJoystickDistance - Joystick.ActualWidth / 2);
                    Canvas.SetTop(Joystick, firstPoint.Position.Y + Math.Sin(angle) * maxJoystickDistance - Joystick.ActualHeight / 2);
                }
            }
        }

        private void StreetCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            JoystickBorder.Visibility = Visibility.Collapsed;
            Joystick.Visibility = Visibility.Collapsed;
            firstPoint = null;
        }  

        private void dispatcherTimer_Tick(object sender, object e)
        {
            dateTimer = dateTimer.AddSeconds(1);
            Timer.Text = dateTimer.Hour.ToString() + ":" + dateTimer.Minute.ToString() + ":" + dateTimer.Second.ToString("D2");
        }

        private void AbilityList_ItemClick(object sender, ItemClickEventArgs e)
        {
            (e.ClickedItem as Ability).Opacity = 0.75;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ability.Opacity)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
