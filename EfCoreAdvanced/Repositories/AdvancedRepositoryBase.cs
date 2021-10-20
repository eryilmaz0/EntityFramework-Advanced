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
    public class AdvancedRepositoryBase<TContext, TEntity> : IRepositoryBase<TContext, TEntity> 
                                                             where TContext : DbContext, new()
                                                             where TEntity : EntityBase<int>, new()
    {
        private TContext _context;
        private IQueryable<TEntity> _entities;

        public AdvancedRepositoryBase()
        {
            this._context = new TContext();
            this._entities = _context.Set<TEntity>();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null, 
                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, 
                            bool disableTracking = true)

        {
            IQueryable<TEntity> query = _entities;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if(include != null)
            {
                query = include(query);
            }

            if(orderby != null)
            {
                query = orderby(query);
            }

            return query.FirstOrDefault(filter);
        }



        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, 
                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, 
                                    bool disableTracking = true)
        {
            IQueryable<TEntity> query = _entities;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }



        public PagedResult<TEntity> GetPaged(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, bool disableTracking = true, PagingParams pagingParams = null)
        {

            IQueryable<TEntity> query = _entities;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }


            int skip = 0;
            int take = 100;

            if(pagingParams != null)
            {
                int pageNumber = pagingParams.PageNumber == 0 ? 1 : pagingParams.PageNumber;
                int pageSize = pagingParams.PageSize == 0 ? 100 : pagingParams.PageSize;

                skip = (pageNumber - 1) * pageSize;
                take = pageSize;
            }

            return new PagedResult<TEntity>()
            {
                TotalCount = query.Count(),
                Result = query.Skip(skip).Take(take).ToList()
            };
        }



        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }



        public void Remove(int entityId)
        {
            TEntity entity = this.Get(filter : x => x.Id == entityId);
            this.Remove(entity);
        }



        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }



        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
    }
}
