using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;
using QuizballApp.DTO.GameDTO;
///<summary>
///This class extends the BaseRepository<T> abstract class and also implements
///the IGameRepository Interface providing all the needed functionality to
///the Game Entity related operations. Instances can be made out of this class.
///<summary>

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
            var games = await _context.Games
                            .Where(g => g.GamemasterId == gamemasterId && g.Participants.Any(p => p.Id == participantId1 || p.Id == participantId2))
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

        public async Task<bool> AddParticipantsToGame(int gameId, Participant participant1, Participant participant2)
        {
            bool isParticipantIn = true;

            var game = await _context.Games
                .Include(g => g.Questions)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            game!.Participants.Add(participant1);
            game.Participants.Add(participant2);

            if (!game.Participants.Contains(participant1) || !game.Participants.Contains(participant2)) isParticipantIn = false;

            return isParticipantIn;
        }
    }
}

