using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UoW.CORE.Interface;
using UoW.LOG;

namespace UoW.DATA
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class, IEntity
    {
        private readonly UoWDbContext context;
        public UnitOfWork(UoWDbContext context)
        {
            this.context = context;
            Repository = new Repository<T>(context);
        }
        public IRepository<T> Repository { get; }
        public IRepository<T> RepositoryLog { get; }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        { 
            context.Dispose();
        }
    }
}
