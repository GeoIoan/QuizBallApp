using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        public readonly QuizballDbContext _context;
        private readonly DbSet<T> _table;

        public BaseRepository(QuizballDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existingEntity = await _table.FindAsync(id);
            if (existingEntity is null)
            {
                return false;
            }
            _table.Remove(existingEntity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _table.ToListAsync();
            return entities;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            var entity = await _table.FindAsync(id);
            return entity;
        }

        public virtual void UpdateAsync(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

