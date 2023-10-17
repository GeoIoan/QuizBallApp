using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

public partial class Gamemaster
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
