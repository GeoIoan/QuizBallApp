using System;
using System.Collections.Generic;
///<summary>
///This class represents the category entity.Instances can be made
///out of this class that can hold data like the category name 
///and the number of the diffculty levels each category has.
///</summary>

namespace QuizballApp.Data;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public int? DifficultyLevels { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
