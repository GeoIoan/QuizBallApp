
namespace QuizballApp.DTO.GameDTO
{
    ///<summary>
    ///Instances of this class can be used
    ///to transfer data needed in order to
    ///complete the operation of getting all
    ///the games that are related with two 
    ///specific participants.
    ///</summary>>
    public class GetGameByParticipantsDTO
    {
        public int GamemasterId { get; set; }

        public int Participant1Id { get; set; }

        public int Participant2Id { get; set; }
    }
}
