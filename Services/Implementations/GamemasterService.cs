using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO.GamemasterDTO;
using Serilog;
using UsersApp.Security;

namespace QuizballApp.Services
{
    public class GamemasterService : IGamemasterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public GamemasterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterService>(); 
        }

        public async Task<bool> ChangeGamemasterPassAsync(ChangeGamemasterPassDTO dto)
        {
            try
            {
                var didPassChange = await _unitOfWork.GamemasterRepository.ChangeGamemasterPasswordAsync(dto);

                await _unitOfWork.SaveAsync();
                
                if (!didPassChange) 
                {

                    _logger.Error("Cound not change pass of gm with id " + dto.Id + " due to an unexpected error");
                    return false;
                }

                var gm = await _unitOfWork.GamemasterRepository.GetAsync(dto.Id);


                var isValid = EncryptionUtil.IsValidPassword(dto.Password!, gm!.Password!);

                if (!isValid)
                {
                    _logger.Error("Cound not change pass of gm with id " + dto.Id + " due to an unexpected error");
                    return false;
                }

                return true;
            }
            catch (DbException)
            {
                _logger.Error("Cound not change pass of gm with id " + dto.Id + " due to a server error");
                throw;
            }
        }

        public string CreateGamemasterToken(int gmId, string gmName, string appSecurityKey)
        {
            Console.WriteLine(appSecurityKey);
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsInfo = new List<Claim>();
            claimsInfo.Add(new Claim(ClaimTypes.Name, gmName));
            claimsInfo.Add(new Claim(ClaimTypes.NameIdentifier, gmId.ToString()));
            var jwtSecurityToken = new JwtSecurityToken(null, null, claimsInfo, DateTime.UtcNow, DateTime.UtcNow.AddHours(3), signingCredentials);

            var gmToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return "Bearer " + gmToken;
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
               
            } catch (DbException)
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
            catch(DbException)
            {
                _logger.Error("Could not get gamemaster " + id + " due to a server error");
                throw;
            }
        }

        public async Task<bool> IsEmailAvailable(CheckEmailDTO dto)
        {
            try
            {
                var isAvailable = await _unitOfWork.GamemasterRepository.CheckEmailAsync(dto);
                await _unitOfWork.SaveAsync();
                return isAvailable;
            }
            catch (DbException)
            {
                _logger.Error("Could not check email " + dto.Email + " due to a server error");
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
            catch (DbException)
            {
                _logger.Error("Could not check username " + username + " due to a server error");
                throw;
            }
        }

        public async Task<Gamemaster?> SingUpAsync(CreateGamemasterDTO dto)
        {
            try
            {
                var gmToInsert = _mapper.Map<Gamemaster>(dto);
                gmToInsert.Password = EncryptionUtil.Encrypt(gmToInsert.Password!);
                var gm = await _unitOfWork.GamemasterRepository.AddAsync(gmToInsert);
                await _unitOfWork.SaveAsync();
                if (gm == null)
                {
                    _logger.Error("Something went wrong whille signing up gamemaster with " + dto.Username);
                    return null;
                }
                return gm;
            }
            catch (DbException)
            {
                _logger.Error("Cound not sign up user with username " + dto.Username + " due to a server error");
                throw;
            }
        }

        public async Task<UpdateGamemasterReadOnlyDTO> UpdateGamemasterAsync(UpdateGamemasterDTO dto)
        {
            try
            {
                
                var updatedGm = await _unitOfWork.GamemasterRepository.UpdateGamemasterAsync(dto);
                await _unitOfWork.SaveAsync();

                var gm = await _unitOfWork.GamemasterRepository.GetAsync(dto.Id);
                

                if(updatedGm is null || !AreEquivalent(gm!, updatedGm))
                {
                    _logger.Error("Something went wrong whille updating gamemaster with " + dto.Id);
                    return null!;
                } 

                return updatedGm;
            }
            catch (DbException)
            {
                _logger.Error("Cound not update gm with id " + dto.Id + " due to a server error");
                throw;
            }
        }

        private bool AreEquivalent(Gamemaster gm, UpdateGamemasterReadOnlyDTO dto)
        {
            return gm.Id == dto.Id &&
                   gm.Username == dto.Username &&
                   gm.Email == dto.Email;
        }
    }
}
