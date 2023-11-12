using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GameDTO
{
    public class GamesEndDTO : BaseDTO
    {
        public string? Winner { get; set; }
        public DateTime? EndDate { get; set; }

        public TimeSpan? Duration { get; set; }
    }
}
