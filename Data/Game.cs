using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

public partial class Game
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public string? Winner { get; set; }

    public string? Score { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Duration { get; set; }

    public int GamemasterId { get; set; }

    public byte? Custom { get; set; }

    public int? Participant1Id { get; set; }

    public int? Participant2Id { get; set; }

    public virtual Gamemaster Gamemaster { get; set; } = null!;

    public virtual Participant? Participant1 { get; set; }

    public virtual Participant? Participant2 { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();


}
