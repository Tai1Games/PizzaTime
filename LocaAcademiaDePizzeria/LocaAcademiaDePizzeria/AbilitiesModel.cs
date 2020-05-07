using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace LocaAcademiaDePizzeria
{
    public class Ability
    {
        public string Image;
        public string ButtonImage = "assets/ManualView/Button.png";
        public double Opacity = 1;
    }

    public class AbilitiesModel
    {
        public static List<Ability> abilities = new List<Ability>()
        {
            new Ability()
            {
                Image = "assets/Abilities/AimAbiolity.png"
            },
            new Ability()
            {
                Image = "assets/Abilities/EyeAbility.png"
            },
            new Ability()
            {
                Image = "assets/Abilities/PizzaAbility.png"
            },
            new Ability()
            {
                Image = "assets/Abilities/SoundAbility.png"
            },
            new Ability()
            {
                Image = "assets/Abilities/TurboAbility.png"
            }
        };

    }
}