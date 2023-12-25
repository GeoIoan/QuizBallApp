namespace QuizBall.Repositories
{

    ///<summary>
    ///This interface is based on the UnitOfWork pattern.
    ///It contains all the repositories as fields and a method
    ///that asychronously saves transactions.The point of this pattern
    ///is to manage all the transactions using one class.
    ///</summary>

    public interface IUnitOfWork
    {
        public GameRepository GameRepository { get; }
        public GamemasterRepository GamemasterRepository { get; }
        public QuestionRepository QuestionRepository { get; }
        public ParticipantRepository ParticipantRepository { get; }

        public CategoryRepository CategoryRepository { get; }

        /// <summary>
        /// Asychronously saves transaction
        /// </summary>
        /// <returns>(bool) True is save False if not</returns>
        Task<bool> SaveAsync();

    }
}
