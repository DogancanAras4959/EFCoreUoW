using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UoW.CORE.Interface;

namespace UoW.LOG
{
    public class UnitOfWorkLog<T> : IUnitOfWorkLog<T> where T : class, IEntity
    {
        private readonly UoWDbLogContext _contextLog;
        public UnitOfWorkLog(UoWDbLogContext contextLog)
        {
            this._contextLog = contextLog;

            RepositoryLog = new RepositoryLog<T>(contextLog);
        }
        public IRepository<T> RepositoryLog { get; }

        public async Task Commit()
        {
            await _contextLog.SaveChangesAsync();
        }

        public void Dispose()
        {
            _contextLog.Dispose();
        }
    }
}
