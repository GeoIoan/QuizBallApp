namespace QuizballApp.DTO.ParticipantsDTO
{

    ///<summary>
    ///Instances of this class
    ///transfer the data needed to
    ///complete the operation of 
    ///changing a participants name.
    ///</summary>>


    public class ChangeParticipantsNameDTO
    {
        public int ParticipantId { get; set; }
        public int GamemasterId { get; set; }
        public string? Name { get; set; }

        public override string? ToString()
        {
            return $"GmId: {GamemasterId}, Name: {Name}, ParticipantId: {ParticipantId}";
        }
    }
}
