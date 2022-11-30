using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.BaseReponsitory
{
    public interface IReponsitoryBase<T>
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void Delete(T entity);
        void Create(T entity);
    }

}
