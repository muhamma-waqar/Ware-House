using Application.Common.Dependencies.DataAccess;
using Application.Common.Dependencies.DataAccess.Repositories.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetProductsList
{
    public class GetProductListQuery : ListQueryModel<ProductDto>
    {
        public ProductStatus status {  get; set; }
        public bool StockedOnly => status == ProductStatus.Stocked;
        public enum ProductStatus
        {
            Default,
            Stocked
        }
    }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IListResponseModel<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;
        public Task<IListResponseModel<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
          => _unitOfWork.Products.GetProjectedListAsync(request,
              additionalFilter: request.StockedOnly ? x => x.NumberInStock > 0 : null,
              readOnly: true);
    }
}
