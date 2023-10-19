using System.ComponentModel.DataAnnotations;

namespace QuizballApp.DTO
{
    public class CreateParticipantDTO
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name can be between 3 and 50 characters"),
          Required(ErrorMessage = "This field is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Type { get; set; }
        public int Members { get; set; }
        public int Wins { get; set; }
        public int GamemasterId { get; set; }
    }
}
