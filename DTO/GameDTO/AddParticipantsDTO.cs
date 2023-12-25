using QuizballApp.Data;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GameDTO
{
    ///<summary>
    ///Instances of this class are used
    ///to transfer the needed to complete
    ///the operation that creates a relation
    ///between a game and two participants
    ///</summary>
    public class AddParticipantsDTO : BaseDTO
    {
        public Participant? Participant1 { get; set; }
        public Participant? Participant2 { get; set; }
    }
}
