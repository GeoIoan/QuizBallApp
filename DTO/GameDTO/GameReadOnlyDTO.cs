using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GameDTO
{
    public class GameReadOnlyDTO : BaseDTO
    {
        public string? Type { get; set; }

        public string? Winner { get; set; }

        public string? Score { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? Duration { get; set; }

        public int GamemasterId { get; set; }

        public byte? Custom { get; set; }
    }
}
