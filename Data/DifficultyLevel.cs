using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

public partial class DifficultyLevel
{
    public int Id { get; set; }

    public string? Level { get; set; }

    public int? Points { get; set; }

    public TimeSpan? Time { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
