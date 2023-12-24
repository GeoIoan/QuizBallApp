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
using QuizballApp.Services;

///<summary>
///This class is used to controll the incoming requests
///concerning the Category Entity. Its methods conduct the
///validation proccess, if needed, and then proceed to call the
///nessecary methods of the service layer.
///</summary>>

namespace QuizballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public CategoriesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Runs asychronously and calls the nessecary service layer
        /// method so that returns all the categories of the game.
        /// </summary>
        /// <returns>(IActionResult) The repsonse to the request</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _applicationService.categoryService.GetAllCatAsync();
                if (categories.IsNullOrEmpty()) return StatusCode(500, "Something went wrong");
                return Ok(categories);
            }catch(DbException)
            {
                return StatusCode(500, "Db failure");
            }
        }
    }
}
