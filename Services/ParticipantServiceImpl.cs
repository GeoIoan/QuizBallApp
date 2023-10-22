using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO;
using Serilog;

namespace QuizballApp.Services
{
    public class ParticipantServiceImpl : IParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public ParticipantServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterServiceImpl>();
        }

        public async Task<ParticipantReadOnlyDTO> ChangeParticipantsNameAsync(int participantId, string name)
        {
            try
            {
                var participant = await _unitOfWork.ParticipantRepository.ChangeParticipantsName(participantId, name);
                await _unitOfWork.SaveAsync();                
                if (participant == null || participant.Name != name)
                {
                    _logger.Error("Something went wrong while updating participants " + participantId + " name");
                    return null!;
                }
                return _mapper.Map<ParticipantReadOnlyDTO>(participant);
            } catch (DbException e)
            {
                _logger.Error("Could not change participants " + participantId + " name to " + name + " due to a server error");
                throw;
            }
        }

        public async Task<bool> CheckParticipantsNameAsync(int gamemasterId, string name)
        {
            try
            {
                var isNameAvailable = await _unitOfWork.ParticipantRepository.CheckParticipantsName(gamemasterId, name);
                return isNameAvailable;
            }
            catch (DbException e)
            {
                _logger.Error("Could check the availability of participants name " + name + " which was made by gamemaster " + gamemasterId + " due to a server error");
                throw;
            }
        }

        public async Task<ParticipantReadOnlyDTO> CreateParticipantAsync(CreateParticipantDTO dto)
        {
            try
            {
                var participant = await _unitOfWork.ParticipantRepository.AddAsync(_mapper.Map<Participant>(dto));
                await _unitOfWork.SaveAsync();
                if (participant == null)
                {
                    _logger.Error("Something went wrong while creating participant type " + dto.Type + " made by gamemaster " + dto.GamemasterId);
                    return null!;
                }
                return _mapper.Map<ParticipantReadOnlyDTO>(participant);
            }
            catch (DbException e)
            {
                _logger.Error("Could not create participant of type " + dto.Type + " mabe by gamemaster " + dto.GamemasterId + " due to server error");
                throw;
            }
        }

        public async Task<ParticipantReadOnlyDTO> DeleteParticipantAsync(int id)
        {
            try
            {
                var deletedParticipant = await _unitOfWork.ParticipantRepository.GetAsync(id);
                var deleted = await _unitOfWork.ParticipantRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

                if (deleted) return _mapper.Map<ParticipantReadOnlyDTO>(deletedParticipant);
                else 
                {
                    _logger.Error("Something went wrong while deleting participant " + id);
                    return null!;
                }
               
            }
            catch (DbException e)
            {
                _logger.Error("Could not delete participant " + id + " due to a server error");
                throw;
            }
        }

        public async Task<List<ParticipantReadOnlyDTO>> GetParticipantsByTypeAsync(int gamemasterId, string type)
        {
            try
            {
                var participants = await _unitOfWork.ParticipantRepository.GetParticipantsByTypeAsync(gamemasterId,type);
                if (participants.IsNullOrEmpty())
                {
                    _logger.Error("Something went wrong whille getting all participants with type " + type +" of gamemaster " + gamemasterId);

                    if (participants == null) return null!;
                }

                var returnedParticipants = new List<ParticipantReadOnlyDTO>();

                participants.ToList().ForEach(p =>
                {
                    returnedParticipants.Add(_mapper.Map<ParticipantReadOnlyDTO>(p));
                });

                return returnedParticipants;
            }
            catch (DbException e)
            {
                _logger.Error("Could not get participants of gamemaster "+ gamemasterId + " with type " + type + " due to a server error");
                throw;
            }
        }
    }
}
