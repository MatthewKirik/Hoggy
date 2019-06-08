using DataAccessLayer.Bases;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public interface IRepository : IDisposable
    {
        IEnumerable<T> GetList<T>() where T : BaseEntity;
        IEnumerable<T> GetList<T>(Func<T, bool> ex) where T : BaseEntity;
        T GetItem<T> (Func<T, bool> ex) where T : BaseEntity;
        bool Contains<T> (Func<T, bool> ex) where T : BaseEntity;
        void Add<T> (T item) where T : BaseEntity;
        void Update<T> (T item) where T : BaseEntity;
        void Delete<T> (T item) where T : BaseEntity;
        void Save();
    }
}
