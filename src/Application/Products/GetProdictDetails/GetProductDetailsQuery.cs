using Application.Common.Dependencies.DataAccess;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetProdictDetails
{
    public record GetProductDetailsQuery : IRequest<ProductDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetProductDetialQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductDetialQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id)
                ?? throw new EntityNotFoundException(nameof(Product), request.Id);

            return _mapper.Map<ProductDetailsDto>(product);
        }
    }
}
