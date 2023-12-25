using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;
using QuizballApp.DTO.GameDTO;
using QuizballApp.Services;

namespace QuizballApp.Controllers
{
    ///<summary>
    ///This class is used to handle all the incoming requests
    ///concerning the Game Entity. Its methods conduct the
    ///validation proccess, if needed, and then proceed to call the
    ///nessecary methods of the service layer.
    ///</summary>

    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public GamesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Runs asychronously and calls the nessecary method
        /// of the service layer that permforms the operation
        /// of creating a new game.
        /// </summary>
        /// <param name="dto">(CreateGameDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpPost("CreateGame")]
        [Authorize]
        public async Task<IActionResult> PostGame(CreateGameDTO dto)
        {
            await Console.Out.WriteLineAsync("We are here");
            if (dto is null) return BadRequest("Invalid data");

            try
            {
                var game = await _applicationService.gameService.CreateGameAsync(dto);

                if (game is null) return StatusCode(500, "Something went wrong");

                return Ok(game);
            }catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        /// <summary>
        /// Runs asychronously and calls the nessecary method
        /// of the service layer that perform the operation
        /// of creating relations between questions and games.
        /// </summary>
        /// <param name="dto">(AddQuestionsDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpPost("AddQuestionsToGame")]
        public async Task<IActionResult> AddQuestions(AddQuestionsDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");
            try
            {
                var areQuestionsAdded = await _applicationService.gameService.AddQuestionsAsync(dto.GameId, dto.Questions);

                if (!areQuestionsAdded) return StatusCode(500, "Something went wrong");

                return Ok();
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        /// <summary>
        /// Runs asychronously and calls the nessecary method
        /// of the service layer that perform the operation
        /// of creating relations between participants and games.
        /// </summary>
        /// <param name="dto">(AddParticipantsDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpPost("AddParticipantsToGame")]
        public async Task<IActionResult> AddParticipants(AddParticipantsDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");
            try
            {
                var areParticipantsAdded = await _applicationService.gameService.AddParticipantsAsync(dto.Id, dto.Participant1!, dto.Participant2!);

                if (!areParticipantsAdded) return StatusCode(500, "Something went wrong");

                return Ok();
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

      /// <summary>
      /// is not used will be deleted
      /// </summary>
      /// <param name="dto"></param>
      /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateGame(GamesEndDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");
            try
            {
                var game = await _applicationService.gameService.UpdateGameAsync(dto);
                if (game is null) return StatusCode(500, "Something went wrong");
                return Ok(game);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }


        /// <summary>
        /// Runs asychronously and calls the nessecary method
        /// of the service layer that permforms the operation
        /// of getting the games that are related with 2 specific
        /// participants.
        /// </summary>
        /// <param name="dto">(GetGameByParticipantsDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpGet(Name = "GetGamesByParticipants")]
        [Authorize]
        public async Task<IActionResult> GetGamesByPaarticipants(GetGameByParticipantsDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");
            try
            {
                var games = await _applicationService.gameService.GetGameByParticipantsAsync(dto);

                if (games.Count == 0) return NotFound();
                if (games is null) return StatusCode(500, "Something went wrong");
                return Ok(games);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

    }
}
