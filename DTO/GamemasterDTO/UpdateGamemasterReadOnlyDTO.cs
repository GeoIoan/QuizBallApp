using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GamemasterDTO
{

    ///<summary>
    ///Instances of this class
    ///are used to return the updated 
    ///data after the gamemaster is updated.
    ///</summary>

    public class UpdateGamemasterReadOnlyDTO : BaseDTO
    {      
        public string? Username { get; set; }
   
        public string? Email { get; set; }
    }
}
