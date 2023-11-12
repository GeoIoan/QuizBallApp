using AutoMapper;
using QuizBall.Repositories;

namespace QuizballApp.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ICategoryService categoryService => new CategoryService(_unitOfWork, _mapper);

        public IGamemasterService gamemasterService => new GamemasterService(_unitOfWork, _mapper);

        public IGameService gameService => new GameService(_unitOfWork, _mapper);

        public IParticipantService participantService => new ParticipantService(_unitOfWork, _mapper);

        public IQuestionService questionService => new QuestionService(_unitOfWork, _mapper);
    }
}
