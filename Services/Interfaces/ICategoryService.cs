using QuizballApp.Data;
using QuizballApp.DTO.CategoryDTO;

namespace QuizballApp.Services
{
    public interface ICategoryService
    {
        Task<IList<CategoryReadOnlyDTO>> GetAllCatAsync();
    }
}
