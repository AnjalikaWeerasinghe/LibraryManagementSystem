using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeProperties = "");

        IQueryable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
