using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Beverages
{
    public class HotChocolate : IBeverage
    {
        public string Name => "hotchocolate";

        public decimal Cost()
        {
            return 3.00m;
        }

        public string Describe()
        {
            return "A warm and comforting hot chocolate.";
        }
    }
}
