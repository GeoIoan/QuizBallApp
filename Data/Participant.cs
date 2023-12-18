///<summary>
/// This class represents the participant entity. Instances can be made out
/// of this class holding data like the participants name, the numbers of the wins
/// the participant achieved and the type of the participant.
///<summary>
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

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual Gamemaster? Gamemaster { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Participant participant &&
               Id == participant.Id &&
               Name == participant.Name &&
               Type == participant.Type &&
               Members == participant.Members &&
               Wins == participant.Wins &&
               GamemasterId == participant.GamemasterId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Type, Members, Wins, GamemasterId);
    }

    public override string? ToString()
    {
        return $"Id: {Id}, Type: {Type}, Members: {Members}, Wins: {Wins}, Gamemaster: {GamemasterId}";
    }


}
