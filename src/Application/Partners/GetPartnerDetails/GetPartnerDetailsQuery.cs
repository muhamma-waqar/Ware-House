using Application.Common.Dependencies.DataAccess;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Partners;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Partners.GetPartnerDetails
{
    public record GetPartnerDetailsQuery : IRequest<PartnerDetailsDto>
    {
        public int Id { get; set; } 
    }

    public class GetPartnerDetailsQueryHandler : IRequestHandler<GetPartnerDetailsQuery, PartnerDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPartnerDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PartnerDetailsDto> Handle(GetPartnerDetailsQuery request, CancellationToken cancellationToken)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException(nameof(Partner), request.Id);

            return _mapper.Map<PartnerDetailsDto>(partner); 
        }
    }
}
