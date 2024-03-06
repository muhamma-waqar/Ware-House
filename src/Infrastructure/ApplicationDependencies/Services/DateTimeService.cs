using Application.Common.Dependencies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationDependencies.Services
{
    internal class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
