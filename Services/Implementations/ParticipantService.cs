﻿using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO.ParticipantsDTO;
using Serilog;

namespace QuizballApp.Services
{

    ///<summary>
    ///Implements that IParticipantService interface. 
    ///Instances can be made out of this class.
    ///</summary>

    public class ParticipantService : IParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterService>();
        }

        public async Task<ParticipantReadOnlyDTO> ChangeParticipantsNameAsync(int participantId, string name)
        {
            try
            {
                await Console.Out.WriteLineAsync("We came to participant service");
                var participant = await _unitOfWork.ParticipantRepository.ChangeParticipantsName(participantId, name);
                await _unitOfWork.SaveAsync();

                await Console.Out.WriteLineAsync("We send our request to the participant repo");
                if (participant == null || participant.Name != name)
                {
                    _logger.Error("Something went wrong while updating participants " + participantId + " name");
                    return null!;
                }
                return _mapper.Map<ParticipantReadOnlyDTO>(participant);
            } catch (DbException)
            {
                _logger.Error("Could not change participants " + participantId + " name to " + name + " due to a server error");
                throw;
            }
        }

        public async Task<bool> CheckParticipantsNameAsync(int? gamemasterId, string name)
        {
            try
            {
                await Console.Out.WriteLineAsync("We are in check participant service method");
                var isNameAvailable = await _unitOfWork.ParticipantRepository.CheckParticipantsName(gamemasterId, name);
                
                await _unitOfWork.SaveAsync();
                await Console.Out.WriteLineAsync("We are in check participant service method2");
                await Console.Out.WriteLineAsync("Is name available: "+ isNameAvailable);
                return isNameAvailable;
            }
            catch (DbException)
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
            catch (DbException)
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
            catch (DbException)
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
            catch (DbException)
            {
                _logger.Error("Could not get participants of gamemaster "+ gamemasterId + " with type " + type + " due to a server error");
                throw;
            }
        }
    }
}
