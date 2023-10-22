using StudentsDBApp.DTO;

namespace QuizballApp.DTO
{
    public class InsertGameToQuestionDTO : BaseDTO
    {
        public string? Type { get; set; }

        public string? Winner { get; set; }

        public string? Score { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TimeSpan? Duration { get; set; }

        public int GamemasterId { get; set; }

        public byte? Custom { get; set; }

        public int? Participant1Id { get; set; }

        public int? Participant2Id { get; set; }
    }
}
