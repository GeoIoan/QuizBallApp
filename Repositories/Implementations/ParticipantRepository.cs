using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(QuizballDbContext context) : base(context) { }
        public async Task<IEnumerable<Participant>> GetParticipantsByTypeAsync(int gamemasterId, string participantType)
        {
            var participants = await _context.Participants.Where(p =>(p.GamemasterId == gamemasterId ) && (p.Type == participantType)).ToListAsync();

            return participants;
        }

        public async Task<Participant?> ChangeParticipantsName(int participantId, string newName)
        {
            var participant = await _context.Participants.Where(p => p.Id == participantId).FirstAsync();
            if (participant is null) return null;

            participant.Name = newName;

            _context.Participants.Update(participant);

            return participant;
        }

        public async Task<bool> CheckParticipantsName(int gamemasterid, string participantsName)
        {
            var participant = await _context.Participants.Where(p => (p.GamemasterId == gamemasterid) && (p.Name == participantsName)).FirstAsync();
            if (participant is null) return true;
            else return false;
        }
    }
}
