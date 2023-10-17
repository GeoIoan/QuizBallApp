namespace QuizBall.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
