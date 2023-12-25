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
    ///<summary>
    ///This class is used to handle all the incoming requests
    ///concerning the Question Entity. Its methods conduct the
    ///validation proccess, if needed, and then proceed to call the
    ///nessecary methods of the service layer.
    ///</summary>

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public QuestionsController(IApplicationService applicationService)
        {
            this._applicationService = applicationService;
        }

        /// <summary>
        /// Runs asychronously and calls a method
        /// from the service layer that performs the
        /// operation of updating an existing question of
        /// a specific gamemaster.
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <param name="dto">(UpdateQuestionDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutQuestion(int id, UpdateQuestionDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");

            try
            {
                if (id != dto.GamemasterId) return Unauthorized();

                var validationErrors = new Dictionary<string, string>();


                var question = await _applicationService.questionService.UpdateCustomQuestionAsync(dto);

                if (question is null) return BadRequest("Something went wrong");

                return Ok(question);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        /// <summary>
        /// Runs asychronously and calls a method
        /// from the service layer that performs the
        /// operation of creating a new question made
        /// by a speicific gamemaster
        /// </summary>
        /// <param name="dto">(CreateQuestionDTO) Contains all the data received from the client
        ///                                that are needed for this operation</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostQuestion(CreateQuestionDTO dto)
        {
            if (dto is null) return BadRequest("Invalid data");

            try
            {

                var question = await _applicationService.questionService.CreateCustomQuestionAsync(dto);

                if (question is null) return StatusCode(500, "Something went wrong");

                return Ok(question);
            }
            catch (DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }

        /// <summary>
        /// Runs asychronously and calls a method
        /// from the service layer that performs 
        /// the operation of deleting a question 
        /// created by specific gamemaster.
        /// </summary>
        /// <param name="id">(int) the id of the question</param>
        /// <returns>(IActionResult) the response to the request</returns>
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


        /// <summary>
        /// Runs asychronously and calls the method
        /// that performs the operation the returns the 
        /// custom questions of a specific gamemaster
        /// </summary>
        /// <param name="id">(int) the id of the gamemaster</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpGet("{id}")]
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


        /// <summary>
        /// Runs asychronously and calls a method 
        /// from the service layer that returns a 
        /// random question of specific category and
        /// difficulty.
        /// </summary>
        /// <param name="gamemasterId">(int) the id of the gamemaster</param>
        /// <param name="categoryId">(int) the id of the category</param>
        /// <param name="difficultyId">(int) the id of the difficulty level</param>
        /// <param name="lastQuestion">(int) the id of the last question
        ///                             that was asked and was meeting that criteria</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpGet] 
        public async Task<IActionResult> GetRandomQuestion(int gamemasterId, int categoryId, int difficultyId, int lastQuestion)
        {
            var dto = new SelectQuestionDTO()
            {
                Gamemaster_id = gamemasterId,
                Category_id = categoryId,
                Difficulty_id = difficultyId,
                LastQuestion = lastQuestion
            };

            await Console.Out.WriteLineAsync("SelectQuestionDto: " + dto);

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

        /// <summary>
        /// we do not use that
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
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

        /// <summary>
        /// Runs asychronously and calls a 
        /// method that perform the operation
        /// of getting all the custom questions
        /// of a specific gamemaster filtered by
        /// category.
        /// </summary>
        /// <param name="gId">(int) the id of a gamemaster</param>
        /// <param name="cId">(int) the id of the category</param>
        /// <returns>(IActionResult) the response to the request</returns>
        [HttpGet("{gId}/{cId}")]
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
