using Application.Common.Dependencies.DataAccess;
using Application.Common.Exceptions;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.UpdateProduct
{
    public record UpdateProductCommand : IRequest
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;

        public float MassValue { get; init; }
        public decimal PriceAmount { get; init; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id)
                ?? throw new EntityNotFoundException(nameof(Product), request.Id);

            product.UpdateName(request.Name.Trim());
            product.UpdateDescription(request.Description.Trim());
            product.UpdateMass(request.MassValue);
            product.UpdatePrice(request.PriceAmount);

            await _unitOfWork.SaveChanges();

            return Unit.Value;
        }
    }
}
