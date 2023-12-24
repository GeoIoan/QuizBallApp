using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizballApp.Data;
using QuizballApp.DTO.ParticipantsDTO;
using QuizballApp.Services;


///<summary>
///This class is used to handle all the incoming requests
///concerning the Participant Entity. Its methods conduct the
///validation proccess, if needed, and then proceed to call the
///nessecary methods of the service layer.
///</summary>
namespace QuizballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ParticipantsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Runs asychronously and calls the method
        /// of the service layer that performs the
        /// operation of creating a new participant
        /// </summary>
        /// <param name="dto">(CreateParticipantDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostParticipant(CreateParticipantDTO dto)
        {
            await Console.Out.WriteLineAsync("" + dto);

            if (dto is null) return BadRequest("Invalid data");

            try
            {
                var isNameAvailable = await _applicationService.participantService.CheckParticipantsNameAsync(dto.GamemasterId, dto.Name!);
                if (!isNameAvailable) return BadRequest("Unavailable name");


                var participant = await _applicationService.participantService.CreateParticipantAsync(dto);

                if (participant is null)
                {
                    return StatusCode(500, "Something went wrong");
                }


                return Ok(participant);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        /// <summary>
        /// Runs asychronously and calls the method
        /// of the service layer that perform the 
        /// operation of deleting an existing participant.
        /// </summary>
        /// <param name="id">(int) the id of the paricipant</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            if (id == 0) return BadRequest("Invalid data");

            try
            {
                var participant = await _applicationService.participantService.DeleteParticipantAsync(id);

                if (participant is null) return StatusCode(500, "Something went wrong");

                return Ok(participant);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        /// <summary>
        /// Runs asychronously and calls the method
        /// of the service layer that performs the 
        /// operation of changing the name of specific
        /// participant who is related to specific 
        /// gamemaster
        /// </summary>
        /// <param name="id">(int) the of the gamemaster</param>
        /// <param name="dto">(ChangeParticipantsNameDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeParticipantsName(int id, ChangeParticipantsNameDTO dto)
        {

            await Console.Out.WriteLineAsync("DTO: " + dto);
            await Console.Out.WriteLineAsync("Id: " + id);

            if (dto is null || id == 0) return BadRequest("Invalid data");
            if (id != dto.GamemasterId) return Unauthorized();

            try
            {
                await Console.Out.WriteLineAsync("We are here!1");
                var isNameAvailable = await _applicationService.participantService.CheckParticipantsNameAsync(id, dto.Name!);
                if (!isNameAvailable) return BadRequest("Unavailable name");

                await Console.Out.WriteLineAsync("We are here!2");
                var participant = await _applicationService.participantService.ChangeParticipantsNameAsync(dto.ParticipantId, dto.Name!);

                
                if (participant is null)
                {
                    return BadRequest("Something went wrong");
                }


                return Ok(participant);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        /// <summary>
        /// Runs asychronously and calls the method
        /// of the service layer that perform the 
        /// operation of getting all the participants
        /// of a specific type that are related to a
        /// specific gamemaster
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <param name="type">(string) the type of the participants</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpGet("{id}/{type}")]
        [Authorize]
        public async Task<IActionResult> GetParticipantByType(int id, string type)
        {
            if (id == 0 || type.IsNullOrEmpty()) return BadRequest("Invalid data");
            try
            {
                var participants = await _applicationService.participantService.GetParticipantsByTypeAsync(id, type);

                if(participants is null) return StatusCode(500,"Something went wrong");
                if(participants.Count == 0) return NotFound();

                return Ok(participants);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }

        }
    }
}
