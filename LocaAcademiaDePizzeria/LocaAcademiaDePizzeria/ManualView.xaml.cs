using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Diagnostics;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class ManualView : Page
    {
        public ManualView()
        {
            this.InitializeComponent();
        }

        public PointerPoint firstPoint = null;
        public int maxJoystickDistance = 60;

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
                if (withinRadius(newPoint))
                {
                    Canvas.SetLeft(Joystick, newPoint.Position.X - Joystick.ActualWidth / 2);
                    Canvas.SetTop(Joystick, newPoint.Position.Y - Joystick.ActualHeight / 2);
                }
                else
                {
                    double angle = Math.Atan2(newPoint.Position.Y - firstPoint.Position.Y, newPoint.Position.X - firstPoint.Position.X);
                    Canvas.SetLeft(Joystick, firstPoint.Position.X + Math.Cos(angle) * maxJoystickDistance - Joystick.ActualWidth / 2);
                    Canvas.SetTop(Joystick, firstPoint.Position.Y + Math.Sin(angle) * maxJoystickDistance - Joystick.ActualHeight / 2);
                }
            }
        }

        private bool withinRadius(PointerPoint newPoint)
        {
            return (Math.Pow(newPoint.Position.X - firstPoint.Position.X, 2) + 
                Math.Pow(newPoint.Position.Y - firstPoint.Position.Y, 2) < Math.Pow(maxJoystickDistance,2));
        }

        private void StreetCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            JoystickBorder.Visibility = Visibility.Collapsed;
            Joystick.Visibility = Visibility.Collapsed;
            firstPoint = null;
        }
    }
}
