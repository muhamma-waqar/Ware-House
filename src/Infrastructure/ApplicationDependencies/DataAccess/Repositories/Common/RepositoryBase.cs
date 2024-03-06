using Application.Common.Dependencies.DataAccess.Repositories.Common;
using Application.Common.Mapping;
using Application.Transactions.GetTransactionsList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Infrastructure.Common.Extensions;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationDependencies.DataAccess.Repositories.Common
{
    internal abstract class RepositoryBaseEF<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected ApplicationDbContext _context;
        protected DbSet<TEntity> _set;
        private readonly IMapper _mapper;


        protected abstract IQueryable<TEntity> BaseQuery { get; }

        public RepositoryBaseEF(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _set = context.Set<TEntity>();
            _mapper = mapper;
        }   

        public virtual async Task<TEntity?> GetByIdAsync(int id)
            => await BaseQuery.SingleOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> filter, bool readOnly = false)
            => await (readOnly ? BaseQuery.AsNoTracking() : BaseQuery).Where(filter).ToListAsync();

        public virtual async Task<TDto?> GetProjectedAsync<TDto>(int id, bool readOnly = false) where TDto : IMapForm<TEntity>
            => await (readOnly ? BaseQuery.AsNoTracking() : BaseQuery)
            .Where(x => x.Id == id)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        public virtual async Task<IListResponseModel<TDto>> GetProjectedListAsync<TDto>(ListQueryModel<TDto> model, Expression<Func<TEntity, bool>>? additionFilter = null, bool readOnly = false) where TDto: IMapForm<TEntity>
        {
            var query = readOnly ? BaseQuery.AsNoTracking() : BaseQuery;

            if(additionFilter != null)
            {
                query = query.Where(additionFilter);
            }

            IQueryable<TDto>? filterDtoQuery = default;
            try
            {
                filterDtoQuery = query.ProjectTo<TDto>(_mapper.ConfigurationProvider)
                    .ApplyFilter(model.Filter);
            }
            catch(FormatException fe)
            {
                model.ThrowFilterIncorrectException(fe.InnerException);
            }

            var totalRowCount = await filterDtoQuery!.CountAsync();
            IEnumerable<TDto>? resultPage = default;
            try
            {
                resultPage = await filterDtoQuery!
                    .ApplyOrder(model.OrderBy)
                    .ApplyPaging(model.PageSize, model.PageIndex)
                    .ToListAsync();
            }
            catch(FormatException fe)
            {
                model.ThrowFilterIncorrectException(fe.InnerException);
            }

            return new ListResponseModel<TDto>(model, totalRowCount, resultPage!);

        }

        public virtual void Add(TEntity entity)
            => _set.Add(entity);

        public virtual void AddRange(IEnumerable<TEntity> entities)
            => _set.AddRange(entities);

        public  virtual void StartTracking(TEntity detachedEntity)
            => _set.Update(detachedEntity);

        public abstract void Remove(TEntity entity);

        public abstract void RemoveRange(IEnumerable<TEntity> entities);

    }
}
