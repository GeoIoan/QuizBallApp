using AutoMapper;
using QuizballApp.Data;
using QuizballApp.DTO;

namespace QuizballApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<Gamemaster, CreateGamemasterDTO>();
            CreateMap<Gamemaster, UpdateGamemasterDTO>();
            CreateMap<Participant, CreateParticipantDTO>();
            CreateMap<Participant, UpdateParticipantDTO>();
            CreateMap<Question, CreateQuestionDTO>();
            CreateMap<Question, UpdateQuestionDTO>();        
        }
    }
}
