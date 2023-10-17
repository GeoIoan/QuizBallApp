namespace QuizBall.Repositories
{
    public interface IUnitOfWork
    {
        public IGameRepository GameRepository { get; }
        public IGamemasterRepository GamemasterRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public IParticipantRepository ParticipantRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        Task<bool> SaveAsync();

    }
}
