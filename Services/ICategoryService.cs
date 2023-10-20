using QuizballApp.Data;

namespace QuizballApp.Services
{
    public interface ICategoryService
    {
        Task<IList<Category>> GetAllCatAsync();
    }
}
