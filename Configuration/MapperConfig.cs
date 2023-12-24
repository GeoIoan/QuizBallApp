using AutoMapper;
using QuizballApp.Data;
using QuizballApp.DTO;
using QuizballApp.DTO.CategoryDTO;
using QuizballApp.DTO.GameDTO;
using QuizballApp.DTO.GamemasterDTO;
using QuizballApp.DTO.ParticipantsDTO;
using QuizballApp.DTO.QuestionDTO;

///<summary>
///This class performs the operation of mapping
///between dtos and and model instances.
///</summary>

namespace QuizballApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<CreateGamemasterDTO, Gamemaster>()
                 .ForSourceMember(x => x.ConfirmedPassword, opt => opt.DoNotValidate());
            CreateMap<CreateParticipantDTO, Participant>();
            CreateMap<Participant, ParticipantReadOnlyDTO>();
            CreateMap<CreateQuestionDTO, Question>();
            CreateMap<UpdateQuestionDTO, Question>();
            CreateMap<Gamemaster, GamemasterReadOnlyDTO>();
            CreateMap<Question, QuestionReadOnlyDTO>();
            CreateMap<InsertGameToQuestionDTO, Game>();
            CreateMap<Game, GameReadOnlyDTO>();
            CreateMap<CreateGameDTO,Game> ();
            CreateMap<Category, CategoryReadOnlyDTO>();
        }
    }
}
