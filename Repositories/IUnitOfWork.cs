namespace QuizBall.Repositories
{
    public interface IUnitOfWork
    {
        public GameRepository GameRepository { get; }
        public GamemasterRepository GamemasterRepository { get; }
        public QuestionRepository QuestionRepository { get; }
        public ParticipantRepository ParticipantRepository { get; }

        public CategoryRepository CategoryRepository { get; }

        Task<bool> SaveAsync();

    }
}
