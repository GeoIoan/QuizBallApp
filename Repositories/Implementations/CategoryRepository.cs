using System.Runtime.CompilerServices;
using QuizballApp.Data;

namespace QuizBall.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(QuizballDbContext context) : base(context) { }
    }
}
