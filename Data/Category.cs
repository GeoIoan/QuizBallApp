using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public int? DifficultyLevels { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
