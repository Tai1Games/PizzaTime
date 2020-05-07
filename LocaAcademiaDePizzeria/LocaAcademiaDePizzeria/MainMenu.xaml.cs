using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace LocaAcademiaDePizzeria
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainMenu : Page
    {
        public Button selectedButton = null;
        public bool isPizzeriaSelected = false;
        public MainMenu()
        {
            this.InitializeComponent();
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            if (isPizzeriaSelected)
            {
                //Go to planning menu.
            }
        }

        private void OnPizzeriaSelected(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != selectedButton)
            {
                if (!isPizzeriaSelected) activatePizzaTimeButton();

                Image img = new Image();
                img.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/PizzeriaSelected.png"));
                img.Stretch = Stretch.Fill;

                StackPanel stackPnl = new StackPanel();
                stackPnl.Children.Add(img);

                button.Content = stackPnl;

                if (selectedButton != null)
                {
                    img = new Image();
                    img.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/PizzeriaUnselected.png"));
                    img.Stretch = Stretch.Fill;

                    stackPnl = new StackPanel();
                    stackPnl.Children.Add(img);

                    selectedButton.Content = stackPnl;
                }
                selectedButton = button;
            }
        }

        private void activatePizzaTimeButton()
        {
            isPizzeriaSelected = true;
            PizzaTimeButtonText.Text = "It's pizza time!";
        }
    }
}
