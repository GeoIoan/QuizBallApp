namespace QuizballApp.DTO.ParticipantsDTO
{
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
