using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using Serilog;

namespace QuizballApp.Services
{
    public class CategoryServiceImpl :  ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public CategoryServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<GamemasterServiceImpl>();
        }

        public async Task<IList<Category>> GetAllCatAsync()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (categories.IsNullOrEmpty()) 
                {
                    _logger.Error("Something went wrong while getting all categories");
                    if (categories == null) return null!;
                }
                
                return categories.ToList();
            }catch (DbUpdateException e)
            {
                _logger.Error("Could not get all categories due to a server error");
                throw;
            }
        }
    }
}
