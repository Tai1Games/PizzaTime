using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaAcademiaDePizzeria
{
    public class Ability
    {
        public int maxLevel { get; set; }

        public int actLevel { get; set; }
        public int upgradeCost { get; set; }

        public string name { get; set; }
      
        public string imgSource { get; set; }

        public Ability() { }

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
            },
            new Ability()
            {
                maxLevel =6,
                actLevel= 4,
                upgradeCost=100,
                name = "Eye",
                imgSource = "Assets//Abilities//EyeAbility.png",

            },
            new Ability()
            {
                maxLevel =12,
                actLevel= 0,
                upgradeCost=100,
                name = "Pizza",
                imgSource = "Assets//Abilities//PizzaAbility.png",
            },
            new Ability()
            {
                maxLevel =6,
                actLevel= 4,
                upgradeCost=100,
                name = "Sound",
                imgSource = "Assets//Abilities//SoundAbility.png",

            },
            new Ability()
            {
                maxLevel =6,
                actLevel= 4,
                upgradeCost=100,
                name = "Turbo",
                imgSource = "Assets//Abilities//TurboAbility.png",

            }
        };

        public static IList<Ability> GetAllAbilities() { return AbilityList; }
    }
}
