using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.BaseReponsitory
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll(int? pageSize, int? pageIndex, Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll();
        Task<IQueryable<T>> GetAllAsQueryable();
        Task<IEnumerable<T>> GetAll(int id);
        Task<List<T>> GetAll(int? pageSize, int? pageIndex);
        Task<int> CountAsync();
        Task<T> FindByName(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<T> GetById(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);
        Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task CreateAsync(T entity);
    }

}
