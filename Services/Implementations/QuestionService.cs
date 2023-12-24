using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO;
using QuizballApp.DTO.QuestionDTO;
using Serilog;

///<summary>
///Implements that IQeustionService interface. 
///Instances can be made out of this class.
///<summary>
namespace QuizballApp.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterService>();
        }

        public async Task<bool> AddGameAsync(int questionId, InsertGameToQuestionDTO dto)
        {
            try
            {
                var added = await _unitOfWork.QuestionRepository.AddGameToQuestion(questionId, _mapper.Map<Game>(dto));
                await _unitOfWork.SaveAsync();

                if (!added)
                {
                    _logger.Error("Something went wrong while adding question " + questionId + " into the " + dto.Id + " game's questions list");
                }

                return added;
            } catch (DbException)
            {
                _logger.Error("A server error occurred whill trying to add the game " + dto.Id + " in the games list of question " + questionId);
                throw;
            }
        }

        public async Task<QuestionReadOnlyDTO> CreateCustomQuestionAsync(CreateQuestionDTO dto)
        {
            try
            {
                var insertedQuestion = await _unitOfWork.QuestionRepository.AddAsync(_mapper.Map<Question>(dto));
                await _unitOfWork.SaveAsync();

                if (insertedQuestion == null)
                {
                    _logger.Error("Something went wrong while inserting the question " + dto.Question1 + " made by gamemaster " + dto.GamemasterId);
                    return null!;
                }

                return _mapper.Map<QuestionReadOnlyDTO>(insertedQuestion);
            }
            catch (DbException)
            {
                _logger.Error("A server error occurred whille trying to add custom question " + dto.Question1 + "of gamemaster " + dto.GamemasterId);
                throw;
            }
        }

        public async Task<QuestionReadOnlyDTO> DeleteCustomQuestionAsync(int id)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetAsync(id);
                var deleted = await _unitOfWork.QuestionRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                if (deleted) return _mapper.Map<QuestionReadOnlyDTO>(question);
                else
                {
                    _logger.Error("Something went wrong while deleting question " + id);
                    return null!;
                }
            }
            catch (DbException)
            {
                _logger.Error("A server error occurred whille trying to delete custom question " + id + " due to server error");
                throw;
            }
        }

        public async Task<IList<QuestionReadOnlyDTO>> GetCustomQuestionsAsync(int gamemasterId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepository.GetCustomQuestionsAsync(gamemasterId);
                if (questions.IsNullOrEmpty())
                {
                    _logger.Error("Something went wrong while geting the new custom questions of gamemaster " + gamemasterId);

                    if (questions == null) return null!;
                }

                var questionToReturn = new List<QuestionReadOnlyDTO>();

                questions.ToList().ForEach(q =>
                {
                    questionToReturn.Add(_mapper.Map<QuestionReadOnlyDTO>(q));
                });

                return questionToReturn;
            }
            catch (DbException)
            {
                _logger.Error("Could not get new custom questions of the gamemaster " + gamemasterId + " due to server error");
                throw;
            }
        }

        public async Task<IList<QuestionReadOnlyDTO>> GetCustQuestionsByCatAsync(int gamemasterId, int catId)
        {
            try
            {
                var questions = await _unitOfWork.QuestionRepository.GetCustQuestionsByCatAsync(catId, gamemasterId);

                if (questions == null)
                {
                    _logger.Error("Something went wrong while getting the custom questions int the " + catId + " category of gamemaster " + gamemasterId);
                    return null!;
                }

                var questionToReturn = new List<QuestionReadOnlyDTO>();

                questions.ToList().ForEach(q =>
                {
                    questionToReturn.Add(_mapper.Map<QuestionReadOnlyDTO>(q));
                });

                return questionToReturn;
            } catch (DbException)
            {
                _logger.Error("Could not get custom questions of the category " + catId + " made by the gamemaster  " + gamemasterId + " due to server error");
                throw;
            }
        }

        public async Task<QuestionReadOnlyDTO> GetRandomQuestionAsync(SelectQuestionDTO dto)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetRandomQuestionAsync(dto.Gamemaster_id, dto.Category_id, dto.Difficulty_id, dto.LastQuestion);
                if (question == null)
                {
                    _logger.Error("Something went wron while sellectin random question based on the criteria gamemaster: " + dto.Gamemaster_id + " , category:  " + dto.Category_id + " , difficulty level: " + dto.Difficulty_id);
                    return null!;
                }

                return _mapper.Map<QuestionReadOnlyDTO>(question);
            }
            catch (DbException)
            {
                _logger.Error("Could not get a random question based on the criteria gamemaster: " + dto.Gamemaster_id + " , category:  " + dto.Category_id + " , difficulty level: " + dto.Difficulty_id + " due to server error");
                throw;
            }
        }

        public async Task<QuestionReadOnlyDTO> UpdateCustomQuestionAsync(UpdateQuestionDTO dto)
        {
            try
            {
                await _unitOfWork.QuestionRepository.UpdateAsync(dto.Id, _mapper.Map<Question>(dto));
                var question = await _unitOfWork.QuestionRepository.GetAsync(dto.Id);
                await _unitOfWork.SaveAsync();

                await Console.Out.WriteLineAsync($"The updated question: " + question);

                if (question is null || !AreEquivalent(question, dto))
                {
                    _logger.Error("Something went wrong while updating question " + dto.Id + " of gamemaster " + dto.Id);
                    return null!;
                }

                return _mapper.Map<QuestionReadOnlyDTO>(question);
            }
            catch (DbException)
            {
                _logger.Error("Could not update question " + dto.Id + " of gamemaster " + dto.GamemasterId + " due to a server error");
                throw;
            }

        }

        private bool AreEquivalent(Question question, UpdateQuestionDTO dto)   
            {
                return question.Id == dto.Id &&
                       question.CategoryId == dto.CategoryId &&
                       question.Media == dto.Media &&
                       question.GamemasterId == dto.GamemasterId &&
                       question.DifficultyId == dto.DifficultyId &&
                       question.Question1 == dto.Question1 &&
                       question.Answers == dto.Answers &&
                       question.FiftyFifty == dto.FiftyFifty;
            }
    }
}
