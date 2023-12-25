﻿using StudentsDBApp.DTO;



namespace QuizballApp.DTO.GamemasterDTO
{
    public class GamemasterReadOnlyDTO : BaseDTO
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
