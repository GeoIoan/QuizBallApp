using StudentsDBApp.DTO;

/// not used
namespace QuizballApp.DTO.GameDTO
{
    public class GamesEndDTO : BaseDTO
    {
        public string? Winner { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime? Duration { get; set; }
    }
}
