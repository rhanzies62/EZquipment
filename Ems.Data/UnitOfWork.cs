using Ems.Domain;
using Ems.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var builder = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        builder.Append($"\"{eve.Entry.Entity.GetType().Name} {ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"\n");
                    }
                }
                throw new Exception(builder.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
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
