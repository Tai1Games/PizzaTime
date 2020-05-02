﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace LocaAcademiaDePizzeria
{
    public class DriverViewModel : Driver
    {
        public Image img;
        public DriverViewModel(Driver model)
        {
            img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + model.imgSource;
            img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));
            img.Width = 50;
            img.Height = 50;
            actDeliveries = model.actDeliveries;
            maxDeliveries = model.maxDeliveries;
            imgSource = model.imgSource;
            name = model.name;
        }
    }
}
