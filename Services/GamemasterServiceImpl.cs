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


        public GamemasterServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task<GamemasterReadOnlyDTO?> GetGamemasterAsync(int id)
        {
            try
            {
                var gm = await _unitOfWork.GamemasterRepository.GetAsync(id);
                await _unitOfWork.SaveAsync();
                if (gm is null)
                {
                    _logger.Error("Something went wrong while getting gamemaster " + id);
                    return null;
                }
                return _mapper.Map<GamemasterReadOnlyDTO>(gm);
            }
            catch(DbException e)
            {
                _logger.Error("Could not get gamemaster " + id + " due to a server error");
                throw;
            }
        }

        public async Task<bool> IsUsernameAvailable(int gamemasterId ,string username)
        {
            try
            {
                var isAvailable =  await _unitOfWork.GamemasterRepository.CheckUsernameAsync(gamemasterId, username);
                await _unitOfWork.SaveAsync();
                return isAvailable;
            }
            catch (DbUpdateException e)
            {
                _logger.Error("Could not check username " + username + " due to a server error");
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
                await Console.Out.WriteLineAsync("Dto for update: " + dto);
                await _unitOfWork.GamemasterRepository.UpdateAsync(dto.Id, _mapper.Map<Gamemaster>(dto));
                await Console.Out.WriteLineAsync("we are here!");
                
                var gm = await _unitOfWork.GamemasterRepository.GetAsync(dto.Id);
               

                await Console.Out.WriteLineAsync($"{gm}");
                await Console.Out.WriteLineAsync("Dto for update: " + dto);

                await _unitOfWork.SaveAsync();

                if (gm is null || !AreEquivalent(gm, dto))
                {
                    _logger.Error("Something went wrong while updating gamemaster " + dto.Id);
                    return null;
                }
                
                return gm;
            }
            catch (DbException e)
            {
                _logger.Error("Cound not sign up user with username " + dto.Username + " due to a server error");
                throw;
            }
        }


        private bool AreEquivalent(Gamemaster gm, UpdateGamemasterDTO dto)
        {
            return gm.Id == dto.Id &&
                   gm.Username == dto.Username &&
                   gm.Password == dto.Password &&
                   gm.Email == dto.Email;
        }
    }
}
