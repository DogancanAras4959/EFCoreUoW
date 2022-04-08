using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UoW.CORE.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task Add(T entity);
        Task Add(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(int id);
        void Delete(IEnumerable<T> entities);
        void Save();
        Task<IList<T>> All();
        Task<IList<T>> AllByObject(string entity);
        Task<IList<T>> AllById(int id);
        Task<T> GetById(int Id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        T FirstOrDefault(Expression<Func<T, bool>> expression = null);
        IQueryable<T> Include(string navigation);

    }
}
