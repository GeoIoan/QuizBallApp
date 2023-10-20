using AutoMapper;
using QuizBall.Repositories;

namespace QuizballApp.Services
{
    public class ApplicationServiceImpl : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public ApplicationServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ICategoryService categoryService => new CategoryServiceImpl(_unitOfWork, _mapper);

        public IGamemasterService gamemasterService => new GamemasterServiceImpl(_unitOfWork, _mapper);

        public IGameService gameService => new GameServiceImpl(_unitOfWork, _mapper);

        public IParticipantService participantService => new ParticipantServiceImpl(_unitOfWork, _mapper);

        public IQuestionService questionService => new QuestionServiceImpl(_unitOfWork, _mapper);
    }
}
