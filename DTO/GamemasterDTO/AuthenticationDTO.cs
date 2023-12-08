using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GamemasterDTO
{
    public class AuthenticationDTO : BaseDTO
    {
        public string? GamemasterName { get; set; }

        public string? GamemasterEmail { get; set; }
        public string? SecurityToken { get; set; }
    }
}
