using Ems.Domain;
using Ems.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private EmsDevEntities context;

        public UnitOfWork()
        {
            this.context = new EmsDevEntities();
        }

        public UnitOfWork(EmsDevEntities context)
        {
            this.context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            //context.Configuration.LazyLoadingEnabled = false; // don't disable lazy loading for now
            return new GenericRepository<TEntity>(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
