using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Beverages
{
    public class Espresso : IBeverage
    {
        public string Name => "espresso";

        public decimal Cost()
        {
            return 2.50m;
        }

        public string Describe()
        {
            return "A strong and bold espresso shot.";
        }
    }
}
