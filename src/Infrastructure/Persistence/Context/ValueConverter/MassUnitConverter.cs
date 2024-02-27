using Domain.Common.Mass;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context.ValueConverter
{
    public class MassUnitConverter : ValueConverter<MassUnit, string>
    {
        public MassUnitConverter() : base(
            massUnit => massUnit.Symbol,
            unitSymbol => MassUnit.FromSymbol(unitSymbol))
        { }
    }
}
