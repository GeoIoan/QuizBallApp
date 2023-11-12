using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using QuizballApp.Data;
using QuizballApp.DTO;
using QuizballApp.DTO.QuestionDTO;
using QuizballApp.Services;

namespace QuizballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public QuestionsController(IApplicationService applicationService)
        {
            this._applicationService = applicationService;
        }

       

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutQuestion(int id, UpdateQuestionDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");

            try
            {
                if (id != dto.GamemasterId) return Unauthorized();

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

                var question = await _applicationService.questionService.UpdateCustomQuestionAsync(dto);

                if (question is null) return BadRequest("Something went wrong");

                return Ok(question);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostQuestion(CreateQuestionDTO dto)
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

                var question = await _applicationService.questionService.CreateCustomQuestionAsync(dto);

                if (question is null) return StatusCode(500, "Something went wrong");

                return Ok(question);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (id == 0) return BadRequest("Invalid data");

            try
            {
                var question = await _applicationService.questionService.DeleteCustomQuestionAsync(id);
                if (question is null)
                {
                    return StatusCode(500, "Something went wrong");
                }

                return Ok(question);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        [HttpGet("{id}", Name = "GetCustomQuestions")]
        [Authorize]
        public async Task<IActionResult> GetCustomQuestions(int id)
        {
            if (id == 0) return BadRequest("Invalid data");

            try
            {
                var questions = await _applicationService.questionService.GetCustomQuestionsAsync(id);
                if (questions.IsNullOrEmpty())
                {
                    return StatusCode(500, "Something went wrong");
                }
                return Ok(questions);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }

        }

        [HttpGet(Name = "GetRandomQuestion")]
        [Authorize]
        public async Task<IActionResult> GetRandomQuestion(SelectQuestionDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");

            try
            {
                var question = await _applicationService.questionService.GetRandomQuestionAsync(dto);

                if (question is null)
                {
                    StatusCode(500, "Something went wrong");
                }
                return Ok(question);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        [HttpPost("{id}", Name = "ConnectQuestionWithGame")]
        [Authorize]
        public async Task<IActionResult> AddGameToQuestion(int id, InsertGameToQuestionDTO dto)
        {
            if (id == 0 || dto is null) return BadRequest("Invalid data");
            
            try {         
                var added = await _applicationService.questionService.AddGameAsync(id, dto);
                if (!added) return StatusCode(500, "Something went wrong");
                return Ok();
            }
            catch(DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        [HttpGet("{gId}/{cId}", Name = "GetCustomQuestionsByCategory")]
        [Authorize]
        public async Task<IActionResult> GetCustomQuestionsByCategory(int gId, int cId)
        {
            if (gId == 0 || cId == 0) return BadRequest("Invalid data");

            try
            {
                var questions = await _applicationService.questionService.GetCustQuestionsByCatAsync(gId, cId);
                if (questions is null) return StatusCode(500, "Something went wrong");

                if (questions.Count == 0) return BadRequest("No categories were found");

                return Ok(questions);
            }
            catch(DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }
    }
}
