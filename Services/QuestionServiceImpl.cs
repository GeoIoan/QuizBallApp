using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO;
using Serilog;

namespace QuizballApp.Services
{
    public class QuestionServiceImpl : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public QuestionServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterServiceImpl>();
        }

        public async Task<bool> AddGameAsync(int questionId, Game game)
        {
            try 
            {
                var added = await _unitOfWork.QuestionRepository.AddGameToQuestion(questionId, game);
                await _unitOfWork.SaveAsync();

                if (!added)
                {
                    _logger.Error("Something went wrong while adding question " + questionId + " into the " + game.Id + " game's questions list");
                }

                return added;
            } catch(DbUpdateException e)
            {
                _logger.Error("A server error occurred whill trying to add the game " + game.Id + " in the games list of question " + questionId);
                throw;
            }
        }

        public async Task<Question> CreateCustomQuestionAsync(CreateQuestionDTO dto)
        {
            try
            {
                var insertedQuestion = await _unitOfWork.QuestionRepository.AddAsync(_mapper.Map<Question>(dto));
                await _unitOfWork.SaveAsync();

                if(insertedQuestion == null)
                {
                    _logger.Error("Something went wrong while inserting the question " + dto.Question + " made by gamemaster " + dto.GamemasterId);
                }

                return insertedQuestion!;
            }
            catch(DbUpdateException e)
            {
                _logger.Error("A server error occurred whille trying to add custom question " + dto.Question + "of gamemaster " + dto.GamemasterId);
                throw;
            }
        }

        public async Task<Question> DeleteCustomQuestionAsync(int id)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetAsync(id);
                var deleted = await _unitOfWork.QuestionRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                if (deleted) return question!;
                else 
                {
                    _logger.Error("Something went wrong while deleting question " + id);
                    return null!; 
                }              
            }
            catch (DbUpdateException e)
            {
                _logger.Error("A server error occurred whille trying to delete custom question " + id + " due to server error");
                throw;
            }
        }

        public async Task<IList<Question>> GetCustomQuestionsAsync(int gamemasterId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepository.GetCustomQuestionsAsync(gamemasterId);
                if (questions.IsNullOrEmpty())
                {
                    _logger.Error("Something went wrong while geting the new custom questions of gamemaster " + gamemasterId);
                    
                    if (questions == null) return null!;
                }
                
                return questions.ToList();
            }catch(DbUpdateException e)
            {
                _logger.Error("Could not get new custom questions of the gamemaster " + gamemasterId + " due to server error");
                throw;
            }
        }

        public async Task<IList<Question>> GetCustQuestionsByCatAsync(int gamemasterId, int catId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepository.GetCustQuestionsByCatAsync(catId, gamemasterId);
                
                if (questions == null)
                {
                    _logger.LogError("Something went wrong while getting the custom questions int the " + catId + " category of gamemaster " + gamemasterId);
                    return null!;
                }
                
                return questions.ToList();
            } catch(DbUpdateException e)
            {
                _logger.LogError("Could not get custom questions of the category " + catId + " made by the gamemaster  " + gamemasterId + " due to server error");
                throw;
            }
        }

        public async Task<Question> GetRandomQuestionAsync(SelectQuestionDTO dto)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetRandomQuestionAsync(dto.Gamemaster_id, dto.Category_id, dto.Difficulty_id);
                if (question == null)
                {
                    _logger.LogError("Something went wron while sellectin random question based on the criteria gamemaster: " + dto.Gamemaster_id + " , category:  " + dto.Category_id + " , difficulty level: " + dto.Difficulty_id);
                    return null!;
                }
                
                return question;
            }catch (DbUpdateException e)
            {
                _logger.LogError("Could not get a random question based on the criteria gamemaster: " + dto.Gamemaster_id + " , category:  " + dto.Category_id + " , difficulty level: " + dto.Difficulty_id + " due to server error");
                throw;
             }
        }

        public async Task<Question> UpdateCustomQuestionAsync(UpdateQuestionDTO dto)
        {
           try
            {
                var question = new Question();
               _unitOfWork.QuestionRepository.UpdateAsync(_mapper.Map<Question>(dto));
                var updatedQuestion = await _unitOfWork.QuestionRepository.GetAsync(dto.Id);
                await _unitOfWork.SaveAsync();
                if (updatedQuestion == null || !updatedQuestion.Equals(dto))
                {
                    _logger.LogError("Something went wrong whill updating question " + dto.Id);
                    return null!;
                }

                return updatedQuestion;
            } catch (DbUpdateException e)
            {
                _logger.LogError("Could not update the question " + dto.Id + " due to a server error");
                throw;
            }
        
        }
    }
}
