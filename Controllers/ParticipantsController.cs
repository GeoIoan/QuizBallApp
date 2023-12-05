﻿using System;
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

        // POST: api/Participants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
       /* [Authorize]*/
        public async Task<IActionResult> PostParticipant(CreateParticipantDTO dto)
        {
            await Console.Out.WriteLineAsync("" + dto);

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

        // DELETE: api/Participants/5
        [HttpDelete("{id}")]
       /* [Authorize]*/
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

        [HttpPut("{id}", Name = "ChangeParticipantsName")]
        /*[Authorize]*/
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


        [HttpGet("{id}/{type}", Name = "GetParticipantByType")]
        /*[Authorize]*/
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
