using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaAcademiaDePizzeria
{
    public class Pizzeria
    {
        public enum state { Selected, Unselected};

        public int X { get; set; }
        public int Y { get; set; }
        public state Estado { get; set; }

        public Pizzeria() { }
    }
    class PizzeriaModel
    {
        public static List<Pizzeria> Pizzerias = new List<Pizzeria>()
        {
            new Pizzeria()
            {
                X = 3,
                Y = 3,
                Estado = Pizzeria.state.Unselected
            },
            new Pizzeria()
            {
                X = 4,
                Y = 4,
                Estado = Pizzeria.state.Unselected
            },
            new Pizzeria()
            {
                X = 5,
                Y = 5,
                Estado = Pizzeria.state.Unselected
            }
        };

        public static IList<Pizzeria> GetAllPizzerias()
        {
            return Pizzerias;
        }
    }

}
