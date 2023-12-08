﻿using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.GamemasterDTO
{
    public class ChangeGamemasterPassDTO : BaseDTO
    {
        [StringLength(32, ErrorMessage = "password should not exceed 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
   ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character")]
        [Required(ErrorMessage = "This field is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? ConfirmedPassword { get; set; }
    }
}
