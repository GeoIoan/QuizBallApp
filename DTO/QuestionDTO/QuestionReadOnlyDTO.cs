﻿using System.ComponentModel.DataAnnotations;
using StudentsDBApp.DTO;

namespace QuizballApp.DTO.QuestionDTO
{
    ///<summary>
    ///Instances of this class contain the data
    ///send as repsonse to the client when the client
    ///requests the creation of a new participant
    ///of the update of an existing one.
    ///</summary>
    public class QuestionReadOnlyDTO : BaseDTO
    {

        public string? Question1 { get; set; }

        public string? Media { get; set; }

        public int CategoryId { get; set; }

        public int? GamemasterId { get; set; }

        public int DifficultyId { get; set; }

        public string? Answers { get; set; }

        public string? FiftyFifty { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is QuestionReadOnlyDTO dTO &&
                   Id == dTO.Id &&
                   Question1 == dTO.Question1 &&
                   Media == dTO.Media &&
                   CategoryId == dTO.CategoryId &&
                   GamemasterId == dTO.GamemasterId &&
                   DifficultyId == dTO.DifficultyId &&
                   Answers == dTO.Answers;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Question1, Media, CategoryId, GamemasterId, DifficultyId, Answers);
        }
    }
}
