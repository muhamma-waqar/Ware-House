using Domain.Common.Mass;
using Domain.Common.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependencies.Services
{
    public interface IStockStatisticsService
    {
        Task<Mass> GetProductStockTotalMass(MassUnit unit);
        Task<Money> GetProductStockTotalValue();
        Task<(int ProductCount, int TotalStock)> GetProductStockCounts();
    }
}
