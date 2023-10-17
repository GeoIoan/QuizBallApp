using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(QuizballDbContext context) : base(context) { } 
       
        public async Task<IEnumerable<Question>> GetCustomQuestionsAsync(int gamemasterId)
        {
            var customQuestions = await _context.Questions
                                .Where(q => q.GamemasterId == gamemasterId)
                                .Where(q => !_context.Games.Any(g => g.GamemasterId == gamemasterId))
                                .ToListAsync();

            return customQuestions;
        }

        public async Task<Question> GetRandomQuestionAsync(int gamemasterId, int categoryId, int difficultyId)
        {
            var filteredQuestionsTask = await _context.Questions.Where(q => (q.GamemasterId == gamemasterId) 
                                                            && (q.CategoryId == categoryId)
                                                            && (q.DifficultyId == difficultyId))
                                                            .ToListAsync();
            
            var filteredQuestions = filteredQuestionsTask;

            var random = new Random();
            int randomIndex = random.Next(0, filteredQuestions.Count);

            return filteredQuestions[randomIndex];
        }
    }
}
