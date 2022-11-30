using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.BaseReponsitory
{
    public abstract class ReponsitoryBase<T> : IReponsitoryBase<T> where T : class
    {
        private readonly MaleFashionDbContext _db;
        public ReponsitoryBase(MaleFashionDbContext db)
        {
            _db = db;
        }
        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _db.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {
                return new List<T>();
            }
        }

        public async Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            var a = await _db.Set<T>().Where(expression).ToListAsync();
            return a;
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Create(T entity)
        {
            _db.Set<T>().Add(entity);
        }
    }
}
