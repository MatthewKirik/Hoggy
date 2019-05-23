using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository : IDisposable
    {
        IEnumerable<T> GetList<T>();
        T GetItem<T> (int id);
        void Add<T> (T item);
        void Update<T> (T item);
        void Delete<T> (int id);
        void Save();
    }
}
