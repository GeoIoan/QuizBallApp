
using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

///<summary>
///This class represents the game entity.Instances can be made
///out of this class that can hold data like the type of the game,
///the score and the duration.
///</summary>
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

    public virtual Gamemaster Gamemaster { get; set; } = null!;
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public override bool Equals(object? obj)
    {
        return obj is Game game &&
               Id == game.Id &&
               Type == game.Type &&
               Winner == game.Winner &&
               Score == game.Score &&
               StartDate == game.StartDate &&
               EndDate == game.EndDate &&
               Duration == game.Duration &&
               GamemasterId == game.GamemasterId &&
               Custom == game.Custom;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Id);
        hash.Add(Type);
        hash.Add(Winner);
        hash.Add(Score);
        hash.Add(StartDate);
        hash.Add(EndDate);
        hash.Add(Duration);
        hash.Add(GamemasterId);
        hash.Add(Custom);
        return hash.ToHashCode();
    }
}
