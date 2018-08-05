using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Domain.Services
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        EmsDevEntities GetDbContext();
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Detach(TEntity entity);
        IQueryable<TEntity> Get(List<Expression<Func<TEntity, bool>>> filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null);
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        IRepositoryQuery<TEntity> Query();
        void Update(TEntity entityToUpdate);
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
        IQueryable<T> SqlQuery<T>(string query, params object[] parameters) where T : class;
        int ExecuteSqlCommand(string query, params object[] parameters);

    }
}
