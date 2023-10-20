﻿namespace QuizballApp.Services
{
    public interface IApplicationService<T>
    {
        ICategoryService categoryService { get; }
       
        IGamemasterService gamemasterService { get; }
        IGameService gameService { get; }
        IParticipantService participantService { get; }
        IQuestionService questionService{ get; }
    }
}
