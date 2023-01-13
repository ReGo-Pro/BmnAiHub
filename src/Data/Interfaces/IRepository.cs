using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Interfaces {
    public interface IRepository<TEntity, TIdentifier> where TEntity : class {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> FindByIdAsync(TIdentifier ID);

        Task<IEnumerable<TEntity>> FindAllAsync();
    }
}
