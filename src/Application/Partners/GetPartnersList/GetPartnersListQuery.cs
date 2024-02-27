using Application.Common.Dependencies.DataAccess;
using Application.Common.Dependencies.DataAccess.Repositories.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Partners.GetPartnersList
{
    public class GetPartnersListQueryHandler : IRequestHandler<ListQueryModel<PartnerDto>, IListResponseModel<PartnerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPartnersListQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Task<IListResponseModel<PartnerDto>> Handle(ListQueryModel<PartnerDto> request, CancellationToken cancellationToken) =>
            _unitOfWork.Partners.GetProjectedListAsync(request, readOnly: true);
    }
}
