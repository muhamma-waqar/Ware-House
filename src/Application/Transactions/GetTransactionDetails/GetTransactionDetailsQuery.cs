using Application.Common.Dependencies.DataAccess;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Transactions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.GetTransactionDetails
{
    public record GetTransactionDetailsQuery : IRequest<TransactionDetailsDto>
    {
        public int Id { get; set; } 
    }

    public class GetTransactionDetailsQueryHandler : IRequestHandler<GetTransactionDetailsQuery, TransactionDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTransactionDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionDetailsDto> Handle(GetTransactionDetailsQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.Transactions.GetProjectedAsync<TransactionDetailsDto>(request.Id, readOnly: true)
            ?? throw new EntityNotFoundException(nameof(Transaction), request.Id);
    }
}
