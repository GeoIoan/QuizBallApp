

using QuizballApp.Data;

namespace QuizBall.Repositories 
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizballDbContext _context;

        public UnitOfWork(QuizballDbContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository => new GameRepository(_context);

        public IGamemasterRepository GamemasterRepository => new GamemasterRepository(_context);

        public IQuestionRepository QuestionRepository => new QuestionRepository(_context);

        public IParticipantRepository ParticipantRepository => new ParticipantRepository(_context);

        public ICategoryRepository CategoryRepository => new CategoryRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
