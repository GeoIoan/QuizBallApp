using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizballApp.Authentication;
using QuizballApp.Data;
using QuizballApp.DTO.GamemasterDTO;
using QuizballApp.Services;

namespace QuizballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamemastersController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IAuth auth;
        private readonly IConfiguration _configuration;

        public GamemastersController(IApplicationService applicationService, IAuth auth, IConfiguration configuration)
        {
            this._applicationService = applicationService;
            this.auth = auth;
            this._configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (dto is null || dto.Username is null || dto.Password is null) return BadRequest("Invalid data");

            var validationErrors = new Dictionary<string, string>();

            if (!TryValidateModel(dto))
            {

                foreach (var key in ModelState.Keys)
                {
                    var modelStateEntry = ModelState[key];
                    if (modelStateEntry!.Errors.Any())
                    {
                        validationErrors[key] = modelStateEntry.Errors.First().ErrorMessage;
                    }
                }

                return BadRequest(validationErrors);
            }
            try
            {

                var authDto = await auth.CheckGamemasterAsync(dto);

                if (authDto is null) return Unauthorized();


                var userToken = _applicationService.gamemasterService.CreateGamemasterToken(authDto.Id, authDto.GamemasterName!, _configuration["Authentication:SecretKey"]!);
                authDto.SecurityToken = userToken;

                return Ok(authDto);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

       

        // GET: api/Gamemasters/5
       
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetGamemaster(int id)
        {
            if (id == 0) return BadRequest("Invalid data");

            try
            {
                var gm = await _applicationService.gamemasterService.GetGamemasterAsync(id);
                if (gm == null)
                {
                    return StatusCode(500, "Something went wrong");
                }
                return Ok(gm);
            }
            catch(DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        // PUT: api/Gamemasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutGamemaster(int id, UpdateGamemasterDTO dto)
        {
            if (id == 0 || dto is null) return BadRequest("Invalid data");

            try { 
                if (id != dto.Id) return Unauthorized();

                var validationErrors = new Dictionary<string, string>();

              


                var isUsernameAvailable = await _applicationService.gamemasterService.IsUsernameAvailable(dto.Id, dto.Username!);
               
                if (!isUsernameAvailable)
                {
                    validationErrors["Username"] = "This username is not available";
                }

               

                if (dto.Password != dto.ConfirmedPassword)
                {
                    validationErrors["ConfirmedPassword"] = "The provided passwords do not match";
                }
                

               
                
                if (validationErrors.Any()) return BadRequest(validationErrors);
                else await Console.Out.WriteLineAsync("Validation dictionary empty");


                var gm = await _applicationService.gamemasterService.UpdateGamemasterAsync(dto);

                if(gm is null)
                {
                    return StatusCode(500, "Something went wrong");
                }

                return Ok("Gamemaste with id " + id + " was updated");
            } catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        // POST: api/Gamemasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<IActionResult> PostGamemaster(CreateGamemasterDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");

            try
            {
                var validationErrors = new Dictionary<string, string>();

                if (!TryValidateModel(dto))
                {

                    foreach (var key in ModelState.Keys)
                    {
                        var modelStateEntry = ModelState[key];
                        if (modelStateEntry!.Errors.Any())
                        {
                            validationErrors[key] = modelStateEntry.Errors.First().ErrorMessage;
                        }
                    }
                    return BadRequest(validationErrors);
                }

                var isUsernameAvailable = await _applicationService.gamemasterService.IsUsernameAvailable(0, dto.Username!);

                if (!isUsernameAvailable)
                {
                    validationErrors["Username"] = "This username is not available";
                }


                if (dto.Password != dto.ConfirmedPassword)
                {
                    validationErrors["ConfirmedPassword"] = "The provided passwords do not match";
                }

                if (validationErrors.Any()) return BadRequest(validationErrors);

                var gm = await _applicationService.gamemasterService.SingUpAsync(dto);

                if (gm is null)
                {
                    return StatusCode(500, "Something went wrong");
                }

                return Ok();
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        // DELETE: api/Gamemasters/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGamemaster(int id)
        {
            if (id == 0) return BadRequest("Invalid data");

            try
            {
                var gm = await _applicationService.gamemasterService.DeleteGamemasterAsync(id);

                if (gm is null)
                {
                    return StatusCode(500, "Something went wrong");
                }

                return Ok("Gamemaster " + id + " was deleted");
            }catch(DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }
    }
}
