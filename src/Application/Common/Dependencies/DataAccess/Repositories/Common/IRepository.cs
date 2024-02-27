using Application.Common.Mapping;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependencies.DataAccess.Repositories.Common
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> filter, bool readOnly = false);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void StartTracking(TEntity entity);

        Task<TDto?> GetProjectedAsync<TDto>(int id, bool readOnly = false) where TDto : IMapForm<TEntity>;

        Task<IListResponseModel<TDto>> GetProjectedListAsync<TDto>(ListQueryModel<TDto> model, Expression<Func<TEntity, bool>>? additionalFilter = null, bool readOnly = false) where TDto : IMapForm<TEntity>;
    }
}
