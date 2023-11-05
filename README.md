# Quizball

Welcome to Quizball, a web application designed to offer an engaging quiz game with questions focused on football knowledge. This application represents a significant milestone for me, as it's the result of my unique idea(the idea of making it an app is mine, the game already exists in analog form), and I've developed it entirely from scratch. Your support and appreciation mean a lot to me, and I sincerely hope you find it enjoyable and engaging.

## Overview

Quizball allows two players or two teams to compete in a football-themed quiz game. The game is skillfully managed and presented by a Gamemaster who is responsible for creating teams and players. The core entities - teams, players, and the game itself - are stored in the database and are closely associated with the Gamemaster, ensuring a seamless gaming experience.

## Gameplay

- Players can choose pre-existing teams and players or create new ones for each game. Past game history is available for previously used teams or players.
- A random selection determines which team or player starts the game.
- Questions are divided into eight categories, each with a specific number of questions and different difficulty levels.
- The selection of questions and their points is based on category and difficulty.
- Custom games can be created by the Gamemaster by inputting 18 custom questions.

## Technology Stack

Quizball is built using the following technology stack:

- ASP.NET Web APIs and asynchronous C#
- SQL Server database with Entity Framework
- Dependency Injection
- Serilog for structured logging

## Entities

- **Participants**: Represent players or teams in the quiz game.
- **Games**: Manage game sessions and store score records.
- **Gamemasters**: Oversee and moderate the game, including team and player management.
- **Questions**: Store quiz questions and their answers.
- **Categories**: Classify questions into eight distinct categories.
- **Difficulty Levels**: Define question difficulty levels (easy, medium, hard).

## Architectural Patterns

- Utilizes the Repository Pattern with a base repository for basic CRUD operations.
- Implements the Unit of Work pattern for interactions with repositories.
- Adopts an Application Service approach for efficient handling of business logic.

## Logging

- Serilog is used for structured logging, capturing essential information, events, and errors within the application.

## Future Frontend Development

In the near future, Quizball's frontend will be enhanced using the Angular framework to create a user-friendly interface with interactive features.

## Dependencies

- **AutoMapper.Extensions.Microsoft.DependencyInjection (Version 12.0.1)**: Library for object-to-object mapping.
- **BCrypt.Net-Core (Version 1.6.0)**: Library for secure password hashing.
- **Microsoft.EntityFrameworkCore.Design (Version 7.0.12)**: Design-time components for Entity Framework Core, essential for database migrations.
- **Microsoft.EntityFrameworkCore.Sqlite (Version 6.0.23)**: Entity Framework Core provider for SQLite, enabling database operations with SQLite.
- **Microsoft.EntityFrameworkCore.SqlServer (Version 7.0.12)**: Entity Framework Core provider for SQL Server, facilitating database operations with SQL Server.
- **Microsoft.EntityFrameworkCore.Tools (Version 7.0.12)**: Additional tools for Entity Framework Core, including database migration support.
- **Microsoft.VisualStudio.Web.CodeGeneration.Design (Version 6.0.16)**: Design-time components for code generation in ASP.NET.
- **Serilog.AspNetCore (Version 7.0.0)**: Integration of Serilog for structured logging within ASP.NET Core.
- **Swashbuckle.AspNetCore (Version 6.5.0)**: Library for generating Swagger/OpenAPI documentation, enhancing the API's accessibility and documentation.

With this comprehensive technology stack and architectural approach, Quizball promises an engaging and customizable quiz gaming experience, further elevated by a future-enhanced frontend developed using the Angular framework.

Thank you for exploring Quizball!
