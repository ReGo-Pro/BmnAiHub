using data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace data.Repositories {
    public class Repository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier> where TEntity : class {
        private DbContext _context;

        public Repository(DbContext context) {
            _context = context;
        }

        public async Task AddAsync(TEntity entity) {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) {
            _context.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync() {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> FindByIdAsync(TIdentifier ID) {
            var entity = await _context.FindAsync(typeof(TEntity), ID);
            if (entity == null) {
                return null;
            }

            return (TEntity)entity;
        }

        public async Task RemoveAsync(TEntity entity) {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities) {
            _context.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        protected virtual DbContext Context => _context;
    }
}
