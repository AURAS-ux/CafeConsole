using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Events
{
    public sealed record OrderPlaced(
        Guid OrderId,
        DateTimeOffset At,
        string Description,
        decimal Subtotal,
        decimal Total
    );
}
