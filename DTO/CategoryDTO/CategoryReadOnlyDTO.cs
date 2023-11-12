using StudentsDBApp.DTO;

namespace QuizballApp.DTO.CategoryDTO
{
    public class CategoryReadOnlyDTO : BaseDTO
    {
        public string? CategoryName { get; set; }

        public int? DifficultyLevels { get; set; }
    }
}
