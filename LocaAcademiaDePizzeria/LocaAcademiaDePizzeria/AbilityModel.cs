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
        public int maxLevel { get; set; }

        public int actLevel { get; set; }
        public int upgradeCost { get; set; }

        public string name { get; set; }

        public string imgSource { get; set; }

        public string description { get; set; }

        public Ability()
        {
        }
    }

    public class AbilityModel
    {
        public static List<Ability> AbilityList = new List<Ability>()
        {
            new Ability()
            {
                maxLevel =5,
                actLevel= 1,
                name = "Aim",
                upgradeCost=100,
                imgSource = "Assets//Abilities//AimAbiolity.png",
                description = "Te permite apuntar con una precision nunca antes conocida",
            },
            new Ability()
            {
                maxLevel =6,
                actLevel= 4,
                upgradeCost=200,
                name = "Eye",
                imgSource = "Assets//Abilities//EyeAbility.png",
                description = "Puedes localizar a los repartidores para evitarlos",
            },
            new Ability()
            {
                maxLevel =12,
                actLevel= 0,
                upgradeCost=150,
                name = "Pizza",
                imgSource = "Assets//Abilities//PizzaAbility.png",
                description = "Tira una pizza para crear una distraccion en la calle",
            },
            new Ability()
            {
                maxLevel =6,
                actLevel= 4,
                upgradeCost=100,
                name = "Sound",
                description = "Emite ultrasonidos para que la gente no se asome a las ventanas y te vea",
                imgSource = "Assets//Abilities//SoundAbility.png",
            },
            new Ability()
            {
                maxLevel =6,
                actLevel= 4,
                upgradeCost=75,
                name = "Turbo",
                description = "Aumenta tu velocidad de forma increible",
                imgSource = "Assets//Abilities//TurboAbility.png",
            }
        };

        public static IList<Ability> GetAllAbilities()
        {
            return AbilityList;
        }
    }
}