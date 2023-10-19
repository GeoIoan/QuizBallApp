using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

public partial class Participant
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? Members { get; set; }

    public int? Wins { get; set; }

    public int? GamemasterId { get; set; }

    public virtual ICollection<Game> GameParticipant1s { get; set; } = new List<Game>();

    public virtual ICollection<Game> GameParticipant2s { get; set; } = new List<Game>();

    public virtual Gamemaster? Gamemaster { get; set; }

    public override string? ToString()
    {
        return $"Id: {Id}, Type: {Type}, Members: {Members}, Wins: {Wins}, Gamemaster: {GamemasterId}";
    }
}
