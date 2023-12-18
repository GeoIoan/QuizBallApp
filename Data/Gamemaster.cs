using System;
using System.Collections.Generic;
///<summary>
///This class represents the gamemaster entity. Intances can be
///made of this class that hold data like the gamemaster username,
///the gamemaster password and the gamemaster email.
///<summary>


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

    public override bool Equals(object? obj)
    {
        return obj is Gamemaster gamemaster &&
               Id == gamemaster.Id &&
               Username == gamemaster.Username &&
               Password == gamemaster.Password &&
               Email == gamemaster.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Username, Password, Email);
    }

    public override string? ToString()
    {
        return $"id: {Id}, username: {Username}, password: {Password}, Email: {Email}";
    }
}
