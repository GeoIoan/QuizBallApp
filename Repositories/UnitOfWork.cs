using QuizballApp.Data;

namespace QuizBall.Repositories
{

    ///<summary>
    ///This class implements the IUnitOfWork interface.
    ///<summary>

    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizballDbContext _context;

        public UnitOfWork(QuizballDbContext context)
        {
            _context = context;
        }

        public GameRepository GameRepository => new GameRepository(_context);

        public GamemasterRepository GamemasterRepository => new GamemasterRepository(_context);

        public QuestionRepository QuestionRepository => new QuestionRepository(_context);

        public ParticipantRepository ParticipantRepository => new ParticipantRepository(_context);

        public CategoryRepository CategoryRepository => new CategoryRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
