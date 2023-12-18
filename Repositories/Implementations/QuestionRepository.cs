using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;
///<summary>
///This class extends the BaseRepository<T> abstract class and also implements
///the IQuestion Interface providing all the needed functionality to
///the Question Entity related operations. Instances can be made out of this class.
///<summary>

namespace QuizBall.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(QuizballDbContext context) : base(context) { } 
       
        public async Task<IEnumerable<Question>> GetCustomQuestionsAsync(int gamemasterId)
        {
            var customQuestions = await _context.Questions.Where(q => q.GamemasterId == gamemasterId &&
                                         _context.Games.All(g => !g.Questions.Contains(q)))
                                        .ToListAsync();

            return customQuestions;
        }

        public async Task<Question> GetRandomQuestionAsync(int? gamemasterId, int categoryId, int difficultyId, int lastQuestion)
        {
            var filteredQuestionsTask = await _context.Questions.Where(q => (q.GamemasterId == gamemasterId) 
                                                            && (q.CategoryId == categoryId)
                                                            && (q.DifficultyId == difficultyId)
                                                            &&(q.Id != lastQuestion))
                                                            .ToListAsync();
            
            var filteredQuestions = filteredQuestionsTask;

            var random = new Random();
            int randomIndex = random.Next(0, filteredQuestions.Count);

            return filteredQuestions[randomIndex];
        }

        public async Task<bool> AddGameToQuestion(int questionId, Game game)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == questionId);

            question!.Games.Add(game);

            await _context.SaveChangesAsync();

            return question!.Games.Contains(game);
        }

        public async Task<IEnumerable<Question>> GetCustQuestionsByCatAsync(int catId, int gamemaster)
        {
            var questions = await _context.Questions.Where(q => (q.GamemasterId == gamemaster) 
                                                           && (q.CategoryId == catId))
                                                           .ToListAsync();

            return questions;
        }
    }
}
