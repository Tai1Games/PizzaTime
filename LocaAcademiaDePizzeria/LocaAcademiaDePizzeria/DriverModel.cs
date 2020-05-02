using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaAcademiaDePizzeria
{
    public class Driver
    {
        public int maxDeliveries { get; set; }

        public int actDeliveries { get; set; }

        public string name { get; set;}

        public string imgSource { get; set; }

        public Driver() { }

    }

    public class DriverModel{
        public static List<Driver> DriverList = new List<Driver>()
        {
            new Driver()
            {
                maxDeliveries=5,
                actDeliveries = 1,
                name = "Vegan Jorge",
                imgSource = "Assets//Samples//8.jpg",
            },
            new Driver()
            {
                maxDeliveries=6,
                actDeliveries = 4,
                name = "Influencer",
                imgSource = "Assets//Samples//17.jpg",

            },
            new Driver()
            {
                maxDeliveries=12,
                actDeliveries = 0,
                name = "Skereeeeee",
                imgSource = "Assets//Samples//7.jpg",
            }
        };

        public static IList<Driver> GetAllDrivers() { return DriverList; }
    }
}
