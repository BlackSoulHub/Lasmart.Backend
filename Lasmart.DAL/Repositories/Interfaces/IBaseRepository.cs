using System.Linq;
using System.Threading.Tasks;
using LanguageExt.Common;
using Lastmart.Domain.Models;

namespace Lasmart.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<Result<TEntity>> CreateAsync(TEntity entity);
        Task<Result<bool>> Delete(TEntity entity);
        Task<Result<TEntity>> UpdateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        IQueryable<TEntity> GetAll();
    }
}