using EfCoreAdvanced.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfCoreAdvanced.Repositories
{
    public interface IRepositoryBase<TContext, TEntity> 
        where TContext : DbContext, new()
        where TEntity : EntityBase<int>, new()

    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, 
                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
                             bool disableTracking = true);

        TEntity Get(Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
                    bool disableTracking = true);


        PagedResult<TEntity> GetPaged(Expression<Func<TEntity, bool>> filter = null,
                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
                                      bool disableTracking = true,
                                      PagingParams pagingParams = null);



        void Insert(TEntity entity);

        void Remove(int entityId);

        void Remove(TEntity entity);

        void Update(TEntity entity);
        
    }
}
