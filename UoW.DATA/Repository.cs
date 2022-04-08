﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UoW.CORE.Interface;

namespace UoW.DATA
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private DbSet<T> Table { get; set; }
        private UoWDbContext _context { get; set; }

        public Repository(UoWDbContext context)
        {
            this._context = context;
            Table = context.Set<T>();          
        }

        public async Task Add(T entity) => await Table.AddAsync(entity);
        public async Task Add(IEnumerable<T> entities) => await Table.AddRangeAsync(entities);
        public async Task<IList<T>> All()
        {
           return await Table.AsNoTracking().ToListAsync();
        }
        public async Task<IList<T>> AllByObject(string entity)
        {
            return await Table.Include(entity).AsNoTracking().ToListAsync();
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }
        public void Delete(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
        }
        public async Task<T> GetById(int Id)
        {
            return await Table.SingleAsync(x=> x.ID == Id);
        }
        public void Update(T entity)
        {
            Table.Update(entity);
        }
        public void Update(IEnumerable<T> entities)
        {
            Table.UpdateRange(entities);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Table.Where<T>(expression);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return Table.FirstOrDefault();
            }
            return Table.FirstOrDefault(expression);
        }

        public void Delete(int id)
        {
            Table.Remove(Table.Find(id));
        }

        public async Task<IList<T>> AllById(int id)
        {
            return await Table.AsNoTracking().Where(x => x.ID == id).ToListAsync();
        }

        public IQueryable<T> Include(string navigation)
        {
            return Table.Include(navigation);
        }
    }
}
