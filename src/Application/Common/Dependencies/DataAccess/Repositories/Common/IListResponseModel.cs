using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependencies.DataAccess.Repositories.Common
{
    public interface IListResponseModel<T>
    {
        int PageIndex { get; }
        int PageSize { get; }

        int PageCount { get; }
        int RowCount { get; }

        string? ActiveFilter { get; }
        string? ActiveOrderBy { get; }

        int FirstRowOnPage { get; }
        int LastRowOnPage { get; }

        IEnumerable<T> Results { get; set; }
    }
}
