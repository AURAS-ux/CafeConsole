using Cafe.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Observers
{
    public class InMemoryOrderAnalytics : IOrderEventSubscriber
    {
        public int OrdersCount { get; set; }
        public decimal Revenue { get; set; }
        public void On(OrderPlaced orderPlaced)
        {
            OrdersCount++;
            Revenue += orderPlaced.Total;
        }
    }
}
