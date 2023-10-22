using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizballApp.Data;
using QuizballApp.Services;

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

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _applicationService.categoryService.GetAllCatAsync();
                if (categories.IsNullOrEmpty()) return StatusCode(500, "Something went wrong");
                return Ok(categories);
            }catch(DbException e)
            {
                return StatusCode(500, "Db failure");
            }
        }
    }
}
