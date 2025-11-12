using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services
{
    public interface IBeverageService
    {
        void Serve(string beverageType);
    }
}
