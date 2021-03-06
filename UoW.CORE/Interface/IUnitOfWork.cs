using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UoW.CORE.Interface
{
    public interface IUnitOfWork<T> : IDisposable where T : class, IEntity
    {
        IRepository<T> Repository { get; }
        Task Commit();
    }
}
