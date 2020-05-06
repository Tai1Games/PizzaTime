using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LocaAcademiaDePizzeria
{
    public class PizzeriaViewModel : Pizzeria
    {
        public PizzeriaViewModel(Pizzeria pizzeria)
        {
            X = pizzeria.X;
            Y = pizzeria.Y;
            Estado = pizzeria.Estado;
        }
    }
}
