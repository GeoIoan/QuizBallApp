using QuizballApp.DTO.GamemasterDTO;

namespace QuizballApp.Authentication
{
    public interface IAuth
    {
        Task<AuthenticationDTO> CheckGamemasterAsync(LoginDTO dto);
    }
}
