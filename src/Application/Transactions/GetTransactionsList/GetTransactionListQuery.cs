using Application.Common.Dependencies.DataAccess;
using Application.Common.Dependencies.DataAccess.Repositories.Common;
using Domain.Transactions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.GetTransactionsList
{
    public class GetTransactionListQuery : ListQueryModel<TransactionDto>
    {
        public TransactionType? Type { get; init; }
    }

    public class GetTransactionListQueryHandleer : IRequestHandler<GetTransactionListQuery, IListResponseModel<TransactionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransactionListQueryHandleer(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IListResponseModel<TransactionDto>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.Transactions.GetProjectedListAsync(request,
                additionalFilter: request.Type.HasValue ? x => x.TransactionType == request.Type : null, readOnly: true);
    }
}
