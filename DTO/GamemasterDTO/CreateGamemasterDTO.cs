﻿using System.ComponentModel.DataAnnotations;
using QuizballApp.Services;

namespace QuizballApp.DTO.GamemasterDTO
{

    ///<summary>
    ///Instances of this class can be used to
    ///complete the operation of creating
    ///a new gamemaster registry.
    ///</summary>
    
    public class CreateGamemasterDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 - 50 characters")]
        [Required(ErrorMessage = "This field is required")]
        public string? Username { get; set; }

        [StringLength(32, ErrorMessage = "password should not exceed 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
    ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character")]
        [Required(ErrorMessage = "This field is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email adress")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Email should be between 10 - 100 characters")]
        public string? Email { get; set; }
    }
}
