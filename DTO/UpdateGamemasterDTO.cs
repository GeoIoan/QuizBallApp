﻿using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO
{
    public class UpdateGamemasterDTO : BaseDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 - 50 characters")]
        [Required(ErrorMessage = ("This field is required"))]
        public string? Username { get; set; }

        [StringLength(32, ErrorMessage = "password should not exceed 32 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
    ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character")]
        [Required(ErrorMessage = ("This field is required"))]
        public string? Password { get; set; }


        [Required(ErrorMessage = ("This field is required"))]
        public string? ConfirmedPassword { get; set; }

        [Required(ErrorMessage = ("This field is required"))]
        [EmailAddress(ErrorMessage = ("Please provide a valid email adress"))]
        public string? Email { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UpdateGamemasterDTO dTO &&
                   Id == dTO.Id &&
                   Username == dTO.Username &&
                   Password == dTO.Password &&
                   Email == dTO.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username, Password, Email);
        }

        public override string? ToString()
        {
            return $"id: {Id}, username: {Username}, password: {Password}, confimredPassword: {ConfirmedPassword}, Email: {Email}";
        }
    }
}
