using StudentsDBApp.DTO;

namespace QuizballApp.DTO
{
    public class CategoryReadOnlyDTO : BaseDTO
    {
        public string? CategoryName { get; set; }

        public int? DifficultyLevels { get; set; }
    }
}
