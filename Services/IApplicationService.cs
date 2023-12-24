///<summary>
///This interface contains all the interfaces concerning 
///the bussiness logic of the app. It is used so that we can
///easily controll all the services of the application.
///</summary>

namespace QuizballApp.Services
{
    public interface IApplicationService
    {
        ICategoryService categoryService { get; }

        IGamemasterService gamemasterService { get; }
        IGameService gameService { get; }
        IParticipantService participantService { get; }
        IQuestionService questionService { get; }
    }
}
