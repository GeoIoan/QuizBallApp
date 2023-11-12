namespace QuizballApp.DTO.GameDTO
{
    public interface GetGameByParticipantsDTO
    {
        public int GamemasterId { get; set; }

        public int Participant1Id { get; set; }

        public int Participant2Id { get; set; }
    }
}
