using DataAccessLayer.Bases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class MsSqlRepository : IRepository
    {
        private DbContext context;

        public MsSqlRepository(DbContext context)
        {
            this.context = context;
        }

        public void Add<T> (T item) where T : BaseEntity
        {
            context.Set<T>().Add(item);
        }

        public void Delete<T>(T item) where T : BaseEntity
        {
            T entity = context.Set<T>().FirstOrDefault(x => x.Id == item.Id);
            if (entity == null)
                return;
            entity.IsDeleted = true;
            Update(entity);
        }

        public IEnumerable<T> GetList<T>() where T : BaseEntity
        {
            return context.Set<T>().Where(x => !x.IsDeleted);
        }

        public IEnumerable<T> GetList<T>(Func<T, bool> ex) where T : BaseEntity
        {
            return context.Set<T>().Where(x => !x.IsDeleted).Where(ex);
        }

        public T GetItem<T>(Func<T, bool> ex) where T : BaseEntity
        {
            return context.Set<T>().Where(x => !x.IsDeleted).FirstOrDefault(ex);
        }

        public bool Contains<T>(Func<T, bool> ex) where T : BaseEntity
        {
            return context.Set<T>().Where(x => !x.IsDeleted).FirstOrDefault(ex) != null;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update<T>(T item) where T : BaseEntity
        {
            context.Entry(item).State = EntityState.Modified;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                context = null;
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MsSqlRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}
