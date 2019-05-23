using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository : IDisposable
    {
        IEnumerable<T> GetList<T>() where T : class;
        IEnumerable<T> GetList<T>(Func<T, bool> ex) where T : class;
        T GetItem<T> (Func<T, bool> ex) where T : class;
        void Add<T> (T item) where T : class;
        void Update<T> (T item) where T : class;
        void Delete<T> (T item) where T : class;
        void Save();
    }
}
