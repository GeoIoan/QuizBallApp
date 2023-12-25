
namespace QuizballApp.DTO.GameDTO
{
    ///<summary>
    ///Instances of this class are used to
    ///transfer the data needed in order
    ///to complete the operation that
    ///creates a new question
    ///</summary>
    public class CreateGameDTO
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
