using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Services
{
    public class ParticipantServiceImpl : IParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ParticipantServiceImpl> _logger;


        public ParticipantServiceImpl(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ParticipantServiceImpl> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Participant> ChangeParticipantsNameAsync(int participantId, string name)
        {
            try
            {
                var participant = await _unitOfWork.ParticipantRepository.ChangeParticipantsName(participantId, name);
                await _unitOfWork.SaveAsync();                
                if (participant == null || participant.Name != name)
                {
                    _logger.LogError("Something went wrong while updating participants " + participantId + " name");
                    return null!;
                }
                return participant!;
            } catch (DbUpdateException e)
            {
                _logger.LogError("Could not change participants " + participantId + " name to " + name + " due to a server error");
                throw;
            }
        }

        public async Task<Participant> CreateParticipantAsync(CreateParticipantDTO dto)
        {
            try
            {
                var participant = await _unitOfWork.ParticipantRepository.AddAsync(_mapper.Map<Participant>(dto));
                await _unitOfWork.SaveAsync();
                if (participant == null)
                {
                    _logger.LogError("Something went wrong while creating participant type " + dto.Type + " made by gamemaster " + dto.GamemasterId);
                    return null!;
                }
                return participant;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError("Could not create participant of type " + dto.Type + " mabe by gamemaster " + dto.GamemasterId + " due to server error");
                throw;
            }
        }

        public async Task<Participant> DeleteParticipantAsync(int id)
        {
            try
            {
                var deletedParticipant = await _unitOfWork.ParticipantRepository.GetAsync(id);
                var deleted = await _unitOfWork.ParticipantRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                if (deleted) return deletedParticipant!;
                else 
                {
                    _logger.LogError("Something went wrong while deleting participant " + id);
                    return null!;
                }
               
            }
            catch (DbUpdateException e)
            {
                _logger.LogError("Could not delete participant " + id + " due to a server error");
                throw;
            }
        }

        public async Task<List<Participant>> GetParticipantsByTypeAsync(int gamemasterId, string type)
        {
            try
            {
                var participants = await _unitOfWork.ParticipantRepository.GetParticipantsByTypeAsync(gamemasterId,type);
                if (participants.IsNullOrEmpty())
                {
                    _logger.LogError("Something went wrong whille getting all participants with type " + type +" of gamemaster " + gamemasterId);

                    if (participants == null) return null!;
                }

                return participants.ToList();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError("Could not get participants of gamemaster "+ gamemasterId + " with type " + type + " due to a server error");
                throw;
            }
        }
    }
}
