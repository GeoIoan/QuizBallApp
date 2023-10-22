using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizballApp.Data;
using QuizballApp.DTO;
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
        [HttpPost]
        public async Task<IActionResult> PostGame(CreateGameDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");

            try
            {
                var game = await _applicationService.gameService.CreateGameAsync(dto);

                if (game is null) return StatusCode(500, "Something went wrong");

                return Ok(game);
            }catch (DbException e)
            {
                return StatusCode(500, "Db failure");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame(GamesEndDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");
            try
            {
                var game = await _applicationService.gameService.UpdateGameAsync(dto);
                if (game is null) return StatusCode(500, "Something went wrong");
                return Ok(game);
            }
            catch (DbException e)
            {
                return StatusCode(500, "Db failure");
            }
        }

        [HttpGet(Name = "GetGamesByParticipants")]
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
            catch (DbException e)
            {
                return StatusCode(500, "Db failure");
            }
        }

    }
}
