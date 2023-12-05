using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;
using QuizballApp.DTO.GameDTO;

namespace QuizBall.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(QuizballDbContext context) : base(context) { }

        public async Task<IEnumerable<Game>> GetAllGamesByGamemasterAsync(int gamemasterId)
        {
            var games = await _context.Games.Where(g => g.GamemasterId == gamemasterId).ToListAsync();
            return games;       
        }

        public async Task<IEnumerable<Game>> GetGameByParticipantsAsync(int gamemasterId, int participantId1, int participantId2)
        {
            var games = await _context.Games.Where(g => (g.GamemasterId == gamemasterId) && 
                                                         (g.Participant1Id == participantId1 
                                                         || g.Participant1Id == participantId2)
                                                         && (g.Participant2Id == participantId1 
                                                         || g.Participant2Id == participantId2))
                                                         .ToListAsync();

            return games;
        }

        public async Task<bool> AddQuestionsToGame(int gameId, List<Question> questions)
        {
            bool isQuestionIn = true;

            var game = await _context.Games
                .Include(g => g.Questions)
                .FirstOrDefaultAsync(g => g.Id == gameId);


            questions.ForEach(q =>
            {
                game!.Questions.Add(q);
            });
           
            await _context.SaveChangesAsync();

            questions.ForEach(q =>
            {
                if (!game!.Questions.Contains(q)) isQuestionIn =  false;
            });

            return isQuestionIn;
        }

        public async Task<Game?> GamesEndUpdate(GamesEndDTO dto)
        {
            var game = await _context.Games.Where(g => g.Id == dto.Id).FirstAsync();
            if (game is null) return null;

            game.Winner = dto.Winner;
            game.EndDate = dto.EndDate;
            game.Duration = dto.Duration;

            _context.Games.Update(game);
            return game;
        }
    }
}

