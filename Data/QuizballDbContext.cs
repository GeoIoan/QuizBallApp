using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuizballApp.Data;

public partial class QuizballDbContext : DbContext
{
    public QuizballDbContext()
    {
    }

    public QuizballDbContext(DbContextOptions<QuizballDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DifficultyLevel> DifficultyLevels { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Gamemaster> Gamemasters { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=QuizballDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CATEGORI__3214EC275863CFED");

            entity.ToTable("CATEGORIES");

            entity.HasIndex(e => e.CategoryName, "IX_CATEGORIES_CATEGORY_NAME");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.DifficultyLevels).HasColumnName("DIFFICULTY_LEVELS");
        });

        modelBuilder.Entity<DifficultyLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DIFFICUL__3214EC273C5313BC");

            entity.ToTable("DIFFICULTY_LEVELS");

            entity.HasIndex(e => e.Level, "IX_DIFFICULTY_LEVELS_LEVEL");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Level)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LEVEL");
            entity.Property(e => e.Points).HasColumnName("POINTS");
            entity.Property(e => e.Time)
                .HasPrecision(0)
                .HasColumnName("TIME");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC27AAB8A95A");

            entity.ToTable("GAMES");

            entity.HasIndex(e => e.GamemasterId, "IX_GAMEMASTER_ID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Custom).HasColumnName("CUSTOM");
            entity.Property(e => e.Duration)
            .HasColumnType("datetime")
            .HasColumnName("DURATION");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.GamemasterId).HasColumnName("GAMEMASTER_ID");
            entity.Property(e => e.Score)
                .HasMaxLength(10)
                .HasColumnName("SCORE");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("TYPE");
            entity.Property(e => e.Winner)
                .HasMaxLength(32)
                .HasColumnName("WINNER");

            entity.HasOne(d => d.Gamemaster).WithMany(p => p.Games)
                .HasForeignKey(d => d.GamemasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GAME_GAMEMASTER");
        });

        modelBuilder.Entity<Gamemaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GAMEMAST__3214EC2799EB63A7");

            entity.ToTable("GAMEMASTERS");

            entity.HasIndex(e => e.Email, "IX_GAMEMASTERS_EMAIL");

            entity.HasIndex(e => e.Username, "IX_GAMEMASTERS_USERNAME");

            entity.HasIndex(e => e.Email, "UQ_GAMEMASTERS_EMAIL").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_GAMEMASTERS_USERNAME").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("EMAIL");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PARTICIP__3214EC270EB635DB");

            entity.ToTable("PARTICIPANTS");

            entity.HasIndex(e => e.GamemasterId, "IX_PARTICIPANTS_GAMEMASTER_ID");

            entity.HasIndex(e => e.Name, "IX_PARTICIPANTS_NAME");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GamemasterId).HasColumnName("GAMEMASTER_ID");
            entity.Property(e => e.Members).HasColumnName("MEMBERS");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("TYPE");
            entity.Property(e => e.Wins).HasColumnName("WINS");

            entity.HasOne(d => d.Gamemaster).WithMany(p => p.Participants)
                .HasForeignKey(d => d.GamemasterId)
                .HasConstraintName("FK_PARTICIPANTS_GAMEMASTERS");

            entity.HasMany(d => d.Games).WithMany(p => p.Participants)
             .UsingEntity<Dictionary<string, object>>(
                "GamesParticipants",
                  r => r.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                         .HasConstraintName("FK_GAME_ID"),
                    l => l.HasOne<Participant>().WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_PARTICIPANT_ID"),
                     je =>
                     {
                         je.HasKey("GameId", "ParticipantId");
                         je.ToTable("GAMES_PARTICIPANTS");
                         je.IndexerProperty<int>("ParticipantId").HasColumnName("PARTICIPANT_ID");
                         je.IndexerProperty<int>("GameId").HasColumnName("GAME_ID");
                     });
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QUESTION__3214EC27E6C2BC34");

            entity.ToTable("QUESTIONS");

            entity.HasIndex(e => e.CategoryId, "IX_QUESTIONS_CATEGORIES");

            entity.HasIndex(e => e.DifficultyId, "IX_QUESTIONS_DIFFICULTY");

            entity.HasIndex(e => e.GamemasterId, "IX_QUESTIONS_GAMEMASTER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Answers)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ANSWERS");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.DifficultyId).HasColumnName("DIFFICULTY_ID");
            entity.Property(e => e.GamemasterId).HasColumnName("GAMEMASTER_ID");
            entity.Property(e => e.Media)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("MEDIA");
            entity.Property(e => e.Question1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("QUESTION");
            entity.Property(e => e.FiftyFifty)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("FIFTY_FIFTY");

            entity.HasOne(d => d.Category).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_QUESTIONS_CATEGORIES");

            entity.HasOne(d => d.Difficulty).WithMany(p => p.Questions)
                .HasForeignKey(d => d.DifficultyId)
                .HasConstraintName("FK_QUESTIONS_DIFFICULTY");

            entity.HasOne(d => d.Gamemaster).WithMany(p => p.Questions)
                .HasForeignKey(d => d.GamemasterId)
                .HasConstraintName("FK_QUESTIONS_GAMEMASTERS");

            entity.HasMany(d => d.Games).WithMany(p => p.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "GamesQuestion",
                    r => r.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GAMES_QUESTIONS_GAME_ID"),
                    l => l.HasOne<Question>().WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GAMES_QUESTIONS_QUESTION_ID"),
                    j =>
                    {
                        j.HasKey("QuestionId", "GameId").HasName("PK__GAMES_QU__54F4F9E9A06BB7F6");
                        j.ToTable("GAMES_QUESTIONS");
                        j.HasIndex(new[] { "QuestionId", "GameId" }, "IX_GAMESQUESTIONS_QUESTIONID_GAMEID");
                        j.HasIndex(new[] { "GameId" }, "IX_GAMES_QUESTIONS_GAMEID");
                        j.HasIndex(new[] { "QuestionId" }, "IX_GAMES_QUESTIONS_QUESTIONID");
                        j.IndexerProperty<int>("QuestionId").HasColumnName("QUESTION_ID");
                        j.IndexerProperty<int>("GameId").HasColumnName("GAME_ID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
