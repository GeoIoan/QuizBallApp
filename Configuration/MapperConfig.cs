using AutoMapper;
using QuizballApp.Data;
using QuizballApp.DTO;
using QuizballApp.DTO.CategoryDTO;
using QuizballApp.DTO.GameDTO;
using QuizballApp.DTO.GamemasterDTO;
using QuizballApp.DTO.ParticipantsDTO;
using QuizballApp.DTO.QuestionDTO;

namespace QuizballApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<CreateGamemasterDTO, Gamemaster>()
                 .ForSourceMember(x => x.ConfirmedPassword, opt => opt.DoNotValidate());
            CreateMap<UpdateGamemasterDTO, Gamemaster>()
                 .ForSourceMember(x => x.ConfirmedPassword, opt => opt.DoNotValidate());
            CreateMap<CreateParticipantDTO, Participant>();
            CreateMap<CreateQuestionDTO, Question>();
            CreateMap<UpdateQuestionDTO, Question>();
            CreateMap<Gamemaster, GamemasterReadOnlyDTO>();
            CreateMap<Question, QuestionReadOnlyDTO>();
            CreateMap<InsertGameToQuestionDTO, Game>();
            CreateMap<Game, GameReadOnlyDTO>();
            CreateMap<Category, CategoryReadOnlyDTO>();
        }
    }
}
