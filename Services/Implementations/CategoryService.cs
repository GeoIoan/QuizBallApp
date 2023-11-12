using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Data;
using QuizballApp.DTO.CategoryDTO;
using Serilog;

namespace QuizballApp.Services
{
    public class CategoryService :  ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;


        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = Log.ForContext<CategoryService>();
        }

        public async Task<IList<CategoryReadOnlyDTO>> GetAllCatAsync()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (categories.IsNullOrEmpty()) 
                {
                    _logger.Error("Something went wrong while getting all categories");
                    if (categories == null) return null!;
                }

                var returnedCat = new List<CategoryReadOnlyDTO> ();

                categories.ToList().ForEach(c =>
                {
                    returnedCat.Add(_mapper.Map<CategoryReadOnlyDTO>(c));
                });
                
                return returnedCat;
            }catch (DbException)
            {
                _logger.Error("Could not get all categories due to a server error");
                throw;
            }
        }
    }
}
