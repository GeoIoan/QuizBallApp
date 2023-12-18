using QuizBall.Repositories;
///<summary>
///This class only contains an extensio method
///in order to add the IUnitOfWork in the IOC container
///<summary>

namespace UsersApp.Repositories
{
	public static class RepositoriesDIExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
