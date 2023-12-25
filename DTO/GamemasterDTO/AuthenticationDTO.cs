using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GamemasterDTO
{

    ///<summary>
    ///Instances of this class can be used
    ///to transfer data needed on the client's
    ///side after a gamemaster is authenticated.
    ///</summary>>

    public class AuthenticationDTO : BaseDTO
    {
        public string? GamemasterName { get; set; }

        public string? GamemasterEmail { get; set; }
        public string? SecurityToken { get; set; }
    }
}
