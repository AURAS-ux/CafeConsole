using Cafe.Domain.Beverages;
using Cafe.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Factories
{
    public class BeverageFactory : IBeverageFactory
    {
        public IBeverage CreateBeverage(string beverageType)
        {
            return beverageType.ToLower() switch
            {
                "espresso" => new Espresso(),
                "tea" => new Tea(),
                "hot chocolate" => new HotChocolate(),
                _ => throw new ArgumentException($"Beverage type '{beverageType}' is not recognized.")
            };
        }
    }
}
