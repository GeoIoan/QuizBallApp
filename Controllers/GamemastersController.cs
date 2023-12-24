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

///<summary>
///This class is used to handle all the incoming requests
///concerning the Gamemaster Entity. Its methods conduct the
///validation proccess, if needed, and then proceed to call the
///nessecary methods of the service layer.
///</summary>>

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

        /// <summary>
        /// Runs asychronously and calls the nessecary
        /// method of the service layer the performs
        /// the login operation of a gamemaster.
        /// </summary>
        /// <param name="dto">(LoginDTO) Contains all the data received from the client
        ///                             that are needed for this operation</param>
        /// <returns>(IActionResult) the resposne to the request</returns>
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

       
        /// <summary>
        /// Runs asychronously and calls the nessecary
        /// method of the service layer that permforms 
        /// the operation of getting an existing gamemaster
        /// based on the provided id.
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <returns>(IActionResult) the response to the request</returns>
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

        /// <summary>
        /// Runs asychronously and calls the nessecary
        /// method of the service layer that performs the
        /// operation of updating an existing gamemaster
        /// based on the provided id.
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <param name="dto">(UpdateGamemasterDTO) Contains all the data received from the client
        ///                             that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
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

        /// <summary>
        /// Runs asychronously and calls the nessecary method
        /// of the service layer that performs the operation
        /// of updating creating a new gamemaster.
        /// </summary>
        /// <param name="dto">(CreateGamemasterDTO) Contains all the data received from the client
        ///                             that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
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

        /// <summary>
        /// Runs asychronously and calls the nessecay method
        /// of the Authentication class that performs the operation
        /// of authecticating an existing gamemaster. 
        /// </summary>
        /// <param name="dto">(CheckGamemastersPassDTO) Contains all the data received from the client
        ///                             that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
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


        /// <summary>
        /// Runs asychronously and calls the nessecary method
        /// of the service layer that permform the operation
        /// of changing the password of an existing gamemaster.
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <param name="dto">(ChangeGamemasterPassDTO) Contains all the data received from the client
        ///                             that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
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


        /// <summary>
        /// Runs asychronously and calls the nessecary method
        /// of the service layer that performs the operation
        /// of deleting an existing gamemaster.
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <returns>(IActionResult) the response to the request</returns>
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
