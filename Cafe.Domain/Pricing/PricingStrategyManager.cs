using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Pricing
{
    public class PricingStrategyManager
    {
        private static RegularPricing _regularPricingStrategy = new RegularPricing();
        private static HappyHourPricing _happyHourPricingStrategy = new HappyHourPricing();

        public static IPricingStrategy GetStrategy(PricingStrategy strategy)
        {
            switch(strategy)
            {
                case PricingStrategy.Regular:
                    return _regularPricingStrategy;
                case PricingStrategy.HappyHour:
                    return _happyHourPricingStrategy;
                default:
                    throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null);
            }
        }
    }
}
