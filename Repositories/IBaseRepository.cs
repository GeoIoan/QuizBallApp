namespace QuizBall.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<T?> AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}
