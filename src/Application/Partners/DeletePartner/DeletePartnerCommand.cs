using Application.Common.Dependencies.DataAccess;
using Application.Common.Exceptions;
using Domain.Partners;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Partners.DeletePartner
{
    public record DeletePartnerCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public async Task<Unit> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(request.Id)
                ?? throw new EntityNotFoundException(nameof(Partner), request.Id);

            _unitOfWork.Partners.Remove(partner);
            await _unitOfWork.SaveChanges();

            return Unit.Value;
        }
    }
}
