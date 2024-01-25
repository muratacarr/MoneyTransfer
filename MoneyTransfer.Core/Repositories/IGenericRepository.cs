using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
