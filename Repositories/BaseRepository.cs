using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;

namespace QuizBall.Repositories
{

    ///<summary>
    ///This is an abstract class that implements the IBaseRepository interface.
    ///Every repository that needs to use any method that is implemented here must
    ///extend this class. No instances can be made out of this class.
    ///</summary>

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

        public virtual async Task<T?> AddAsync(T entity)
        {
            var entry = await _table.AddAsync(entity);

            return entry.Entity;
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


        public virtual async Task<T?> UpdateAsync(int id,T entity)
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }

            var updatedEntity = _context.Set<T>().Find(id);

            if (updatedEntity!.Equals(entity)) return updatedEntity;
            else return null;
        }
    }
}

