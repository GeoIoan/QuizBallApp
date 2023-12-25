using QuizBall.Repositories;

namespace UsersApp.Repositories
{

    ///<summary>
    ///This class only contains an extensiom method
    ///in order to add the IUnitOfWork in the IOC container
    ///</summary>

    public static class RepositoriesDIExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
