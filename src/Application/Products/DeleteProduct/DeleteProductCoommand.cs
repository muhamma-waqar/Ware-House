using Application.Common.Dependencies.DataAccess;
using Application.Common.Exceptions;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.DeleteProduct
{
    public record DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id)
                ?? throw new EntityNotFoundException(nameof(Product), request.Id);

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChanges();

            return Unit.Value;
        }
    }
}
