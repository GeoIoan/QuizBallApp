using System;
using System.Collections.Generic;

namespace QuizballApp.Data;

public partial class Question
{
    public Question()
    {
    }

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

    public override bool Equals(object? obj)
    {
        return obj is Question question &&
               Id == question.Id &&
               Question1 == question.Question1 &&
               Media == question.Media &&
               GamemasterId == question.GamemasterId &&
               CategoryId == question.CategoryId &&
               DifficultyId == question.DifficultyId &&
               Answers == question.Answers;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Id);
        hash.Add(Question1);
        hash.Add(Media);
        hash.Add(GamemasterId);
        hash.Add(CategoryId);
        hash.Add(DifficultyId);
        hash.Add(Answers);
        return hash.ToHashCode();
    }

    public override string? ToString()
    {
        return $"id: {Id}, question : {Question1}, media: {Media}, gamemasterid: {GamemasterId}, categoryid: {CategoryId}, difficulty: {DifficultyId}, answer: {Answers}";
    }
}
