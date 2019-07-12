using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ContainsAsync(int id);
    }
}
