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
            catch (DbException)
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
