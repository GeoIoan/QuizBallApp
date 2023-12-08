using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GamemasterDTO
{
    public class UpdateGamemasterDTO : BaseDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 - 50 characters")]
        [Required(ErrorMessage = "This field is required")]
        public string? Username { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email adress")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Email should be between 10 - 100 characters")]
        public string? Email { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UpdateGamemasterDTO dTO &&
                   Id == dTO.Id &&
                   Username == dTO.Username &&
                   Email == dTO.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username,Email);
        }

        public override string? ToString()
        {
            return $"id: {Id}, username: {Username}, Email: {Email}";
        }
    }
}
