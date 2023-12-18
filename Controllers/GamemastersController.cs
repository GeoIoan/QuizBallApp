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
using Microsoft.JSInterop.Infrastructure;
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

                var isEmailAvailable = await _applicationService.gamemasterService.IsEmailAvailable(new CheckEmailDTO()
                { Id = dto.Id, Email = dto.Email });

                if (!isEmailAvailable) validationErrors["Email"] = "This email is not available";

                                        
                if (validationErrors.Any()) return BadRequest(validationErrors);
                else await Console.Out.WriteLineAsync("Validation dictionary empty");


                var gm = await _applicationService.gamemasterService.UpdateGamemasterAsync(dto);

                if(gm is null)
                {
                    return StatusCode(500, "Something went wrong");
                }

                return Ok(gm);
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


                var isUsernameAvailable = await _applicationService.gamemasterService.IsUsernameAvailable(0, dto.Username!);

                if (!isUsernameAvailable)
                {
                    validationErrors["Username"] = "This username is not available";
                }


                if (dto.Password != dto.ConfirmedPassword)
                {
                    validationErrors["ConfirmedPassword"] = "The provided passwords do not match";
                }


                var isEmailAvailable = await _applicationService.gamemasterService.IsEmailAvailable(new CheckEmailDTO()
                { Id = 0, Email = dto.Email });

                if (!isEmailAvailable) validationErrors["Email"] = "This email is not available";

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

        [HttpPost("checkPass")]
        [Authorize]
        public async Task<IActionResult> CheckGamemasterPass(CheckGamemastersPassDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");

          

            if (dto.Password != dto.ConfirmedPassword) return BadRequest("Passwords do not match");

            try
            {
                var authenticated = await auth.CheckGamemasterAsync(new LoginDTO()
                {
                    Username= dto.Username,
                    Password = dto.Password
                });

                await Console.Out.WriteLineAsync("Aythenticated: " + authenticated);

                if (authenticated is null) return Unauthorized();

                return Ok();
            } catch(DbException){
                return StatusCode(500, "Db failure");
            }
        }

        [HttpPut("changePass/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeGamemasterPass(int id, ChangeGamemasterPassDTO dto)
        {
            if(id == 0) return BadRequest("Invalid data");
            if (dto is null) return BadRequest("Invalid data");

            if (id != dto.Id) return Unauthorized();

            if (dto.Password != dto.ConfirmedPassword) return BadRequest("Passwords do not match");
            try
            {
                var didPassChange = await _applicationService.gamemasterService.ChangeGamemasterPassAsync(dto);
               
                if (!didPassChange) return StatusCode(500, "Something went wrong");

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

                return Ok();
            }catch(DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }
    }
}
