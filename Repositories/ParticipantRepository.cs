using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(QuizballDbContext context) : base(context) { }
        public async Task<IEnumerable<Participant>> GetParticipantsByTypeAsync(string participantType)
        {
            var participants = await _context.Participants.Where(p => p.Type == participantType).ToListAsync();

            return participants;
        }
    }
}
