using Application.Common.Dependencies.DataAccess.Repositories;
using AutoMapper;
using Domain.Partners;
using Infrastructure.ApplicationDependencies.DataAccess.Repositories.Common;
using Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationDependencies.DataAccess.Repositories
{
    internal class PartnerRepositoryEF : RepositoryBaseEF<Partner>, IPartnerRepository
    {
        protected override IQueryable<Partner> BaseQuery
            => _context.Partners;

        public PartnerRepositoryEF(ApplicationDbContext context, IMapper mapper): base(context, mapper) { }

        public override void Remove(Partner entity)
        {
            _context.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<Partner> entities)
        {
            foreach(var e in entities)
                this.Remove(e);
        }
    }
}
