using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.ParticipantsDTO
{

    ///<summary>
    ///Instances of this class contain the data
    ///send as repsonse to the client when the client
    ///requests the creation of a new participant.
    ///</summary>

    public class ParticipantReadOnlyDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Type { get; set; }

        public int GamemasterId { get; set; }
        public int? Members { get; set; }
        public int? Wins { get; set; }
    }
}
