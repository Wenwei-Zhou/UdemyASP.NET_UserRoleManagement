using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAndManagement.Repositories
{
    // <T>这是在定义一个泛型接口，T 是你传进来的实体类型。where T : class 泛型约束，表示 T 必须是一个引用类型（class，而不是 int、bool 这种值类型）
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByTypeAsync(string JobType);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}