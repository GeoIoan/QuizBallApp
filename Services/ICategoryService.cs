using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Services
{
    public interface ICategoryService
    {
        Task<IList<CategoryReadOnlyDTO>> GetAllCatAsync();
    }
}
