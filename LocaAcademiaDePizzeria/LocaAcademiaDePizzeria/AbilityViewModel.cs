using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace LocaAcademiaDePizzeria
{
    public class AbilityViewModel : Ability
    {
        public Image img;

        public double getLevelBar()
        {
            return 100 * (double)actLevel / (double)maxLevel;
        }

        public AbilityViewModel(Ability model)
        {
            img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + model.imgSource;
            img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));
            img.Width = 50;
            img.Height = 50;
            actLevel = model.actLevel;
            maxLevel = model.maxLevel;
            imgSource = model.imgSource;
            name = model.name;
            upgradeCost = model.upgradeCost;
            description = model.description;
        }
    }
}