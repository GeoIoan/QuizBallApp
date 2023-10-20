using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO;
using Serilog;

namespace QuizballApp.Services
{
    public class GameServiceImpl : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public GameServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterServiceImpl>();
        }

        public async Task<bool> AddQuestionAsync(int gameId, Question question)
        {
            try
            {
                var added = await _unitOfWork.GameRepository.AddQuestionToGame(gameId, question);
                await _unitOfWork.SaveAsync();
                if (!added)
                {
                    _logger.Error("Something went wrong while adding " + question.Id + "in the list of game" + gameId);
                }
                return added;
            }
            catch (DbUpdateException e)
            {
                _logger.Error("The question " + question.Id + " was not inserted in the list of game " + gameId + " due to server error");
                throw;           
            
            }
        }

        public async Task<Game> CreateGameAsync(CreateGameDTO dto)
        {
            try
            {
                var insertedGame = await _unitOfWork.GameRepository.AddAsync(_mapper.Map<Game>(dto));
                await _unitOfWork.SaveAsync();
                if (insertedGame == null) 
                {
                    _logger.Error("Something went wrong while creating the game of gamemaster " + dto.GamemasterId);
                    return null!;
                } 
                else return insertedGame;
            }
            catch (DbUpdateException e)
            {
                _logger.Error("The game of the gamemaster " + dto.GamemasterId + " could not be created due to server error");
                throw;
            }
        }

        public async Task<IList<Game>> GetGameByParticipantsAsync(GetGameByParticipantsDTO dto)
        {
            try { 
            var games = await _unitOfWork.GameRepository.GetGameByParticipantsAsync(dto.GamemasterId, dto.Participant1Id, dto.Participant2Id);
                if (games.ToList().Count == 0)
                {
                    _logger.Information("No games were found including these participants " + dto.Participant1Id + ", " + dto.Participant2Id);
                } 
                else if (games == null)
                {
                    _logger.Error("Something went wrong while getting the games of participants " + dto.Participant1Id + ", " + dto.Participant2Id);
                    return null!;
                }
                
                return games.ToList();
            }
            catch(DbUpdateException e)
            {
                _logger.Error("Cound not get games whith participants " + dto.Participant1Id + ", " + dto.Participant2Id + " due to a server error");
                throw;
            }
        }

        public async Task<Game> UpdateGameAsync(GamesEndDTO dto)
        {
            try
            {
                var game = await _unitOfWork.GameRepository.GamesEndUpdate(dto);
                await _unitOfWork.SaveAsync();
                if (game == null || dto.Winner != game.Winner)
                {
                    _logger.Error("Something wrong happend while updating game " + dto.Id);
                    return null!;
                }
                return game;    
            } catch (DbUpdateException e)
            {
                _logger.Error("Could not update game " + dto.Id  + " due to server error");
                throw;
            }
        }
    }
}
