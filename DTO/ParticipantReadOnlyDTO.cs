using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO
{
    public class ParticipantReadOnlyDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int Members { get; set; }
        public int Wins { get; set; }
    }
}
