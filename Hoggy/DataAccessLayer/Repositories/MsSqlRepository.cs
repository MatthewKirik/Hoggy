using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MsSqlRepository : IRepository
    {
        private DbContext context;

        public MsSqlRepository(DbContext context)
        {
            this.context = context;
        }

        public void Add<T> (T item) where T : class
        {
            context.Set<T>().Add(item);
        }

        public void Delete<T>(T item) where T : class
        {
            context.Set<T>().Remove(item);
        }

        public IEnumerable<T> GetList<T>() where T : class
        {
            return context.Set<T>();
        }

        public IEnumerable<T> GetList<T>(Func<T, bool> ex) where T : class
        {
            return context.Set<T>().Where(ex);
        }

        public T GetItem<T>(Func<T, bool> ex) where T : class
        {
            return context.Set<T>().FirstOrDefault(ex);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update<T>(T item) where T : class
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
