using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO;
using Serilog;

namespace QuizballApp.Services
{
    public class GamemasterServiceImpl : IGamemasterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public GamemasterServiceImpl(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GamemasterServiceImpl> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterServiceImpl>(); 
        }

        public async Task<Gamemaster?> DeleteGamemasterAsync(int id)
        {
            try
            {
                var deletedGm = await _unitOfWork.GamemasterRepository.GetAsync(id);
                var deleted = await _unitOfWork.GamemasterRepository.DeleteAsync(id);  
               
                await _unitOfWork.SaveAsync();

                if (!deleted)
                {
                    _logger.Error("Something went wrong whille deleting Gamemaster " + id);
                    return null;
                }

                 return deletedGm;
               
            } catch (DbUpdateException e)
            {
                _logger.Error("Could not delete gamemaster " + id + " due to a server error");
                throw;
            }
        }

        public async Task<Gamemaster?> GetGamemasterByUsernameAsync(string username)
        {
            try
            {
                var gm = await _unitOfWork.GamemasterRepository.GetByUsernameAsync(username);
                return gm;
            }
            catch (DbUpdateException e)
            {
                _logger.Error("Could not get gamemaster by username " + username + " due to a server error");
                throw;
            }
        }

        public async Task<Gamemaster?> SingUpAsync(CreateGamemasterDTO dto)
        {
            try
            {
                var gm = await _unitOfWork.GamemasterRepository.AddAsync(_mapper.Map<Gamemaster>(dto));
                await _unitOfWork.SaveAsync();
                if (gm == null)
                {
                    _logger.Error("Something went wrong whille signing up gamemaster with " + dto.Username);
                    return null;
                }
                return gm;
            }
            catch (DbUpdateException e)
            {
                _logger.Error("Cound not sign up user with username " + dto.Username + " due to a server error");
                throw;
            }
        }

        public async Task<Gamemaster?> UpdateGamemasterAsync(UpdateGamemasterDTO dto)
        {
            try
            {
                _unitOfWork.GamemasterRepository.UpdateAsync(_mapper.Map<Gamemaster>(dto));
                var gm = await _unitOfWork.GamemasterRepository.GetAsync(dto.Id);
                await _unitOfWork.SaveAsync();

                if (gm == null || !gm.Equals(dto))
                {
                    _logger.Error("Something went wrong while updating gamemaster " + dto.Id);
                    return null;
                }
                return gm;
            }
            catch (DbUpdateException e)
            {
                _logger.Error("Cound not sign up user with username " + dto.Username + " due to a server error");
                throw;
            }
        }
    }
}
