using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;

namespace QuizBall.Repositories
{

    ///<summary>
    ///This class extends the BaseRepository<T> abstract class and also implements
    ///the IParticipant Interface providing all the needed functionality to
    ///the Participant Entity related operations. Instances can be made out of this class.
    ///</summary>

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

            await Console.Out.WriteLineAsync("We are in the participants repo");
            var participant = await _context.Participants.Where(p => p.Id == participantId).FirstAsync();
            if (participant is null) return null;

            participant.Name = newName;

            _context.Participants.Update(participant);

            await Console.Out.WriteLineAsync("Participant: " + participant);

            return participant;
        }

        public async Task<bool> CheckParticipantsName(int? gamemasterid, string participantsName)
        {
            await Console.Out.WriteLineAsync("We are in check participant name repo method");
            var participant = await _context.Participants.Where(p => (p.GamemasterId == gamemasterid) && (p.Name == participantsName)).FirstOrDefaultAsync();

            

            if (participant is null) return true;
            else return false;
        }
    }
}
