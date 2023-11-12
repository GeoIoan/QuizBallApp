using System.Data.Common;
using QuizBall.Repositories;
using QuizballApp.DTO.GamemasterDTO;
using QuizballApp.Services;
using Serilog;

namespace QuizballApp.Authentication
{
    public class Auth : IAuth
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Serilog.ILogger _logger;

        public Auth(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = Log.ForContext<Auth>();
        }

        public async Task<AuthenticationDTO> CheckGamemasterAsync(LoginDTO dto)
        {
            try
            {
                var gm = await _unitOfWork.GamemasterRepository.CheckGamemasterAsync(dto.Username!, dto.Password!);
                if (gm is null) return null!;
                var authDto = new AuthenticationDTO();
                authDto.Id = gm.Id;
                authDto.GamemasterName = gm.Username;
                return authDto;
            }catch(DbException)
            {
                Log.Error("Could not check gamemasters " + dto.Username + " validity due to a server error");
                throw;
            }
        }
    }
}
