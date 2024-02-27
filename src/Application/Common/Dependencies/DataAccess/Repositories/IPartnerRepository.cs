using Application.Common.Dependencies.DataAccess.Repositories.Common;
using Domain.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependencies.DataAccess.Repositories
{
    public interface IPartnerRepository: IRepository<Partner>
    {
    }
}
