using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GamemasterDTO
{
    ///<summary>
    ///Instances of this class can
    ///be used to transfer data needed
    ///in order to complete the operation
    ///of checking the availability of an email
    ///when a new gamemaster is created
    ///or when an existing one is updated
    ///</summary>>

    public class CheckEmailDTO : BaseDTO
    {
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email adress")]
        public string? Email { get; set; }
    }
}
