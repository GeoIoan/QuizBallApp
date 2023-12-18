using QuizballApp.Data;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GameDTO
{
    public class AddParticipantsDTO : BaseDTO
    {
        public Participant? Participant1 { get; set; }
        public Participant? Participant2 { get; set; }
    }
}
