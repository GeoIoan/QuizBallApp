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
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public GamesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateGame")]
        /* [Authorize]*/
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
