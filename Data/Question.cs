using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

public partial class Question
{
    public int Id { get; set; }

    public string? Question1 { get; set; }

    public string? Media { get; set; }

    public int? GamemasterId { get; set; }

    public int? CategoryId { get; set; }

    public int? DifficultyId { get; set; }

    public string? Answers { get; set; }

    public virtual Category? Category { get; set; }

    public virtual DifficultyLevel? Difficulty { get; set; }

    public virtual Gamemaster? Gamemaster { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
