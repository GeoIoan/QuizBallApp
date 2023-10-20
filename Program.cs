using Microsoft.EntityFrameworkCore;
using QuizBall.Repositories;
using QuizballApp.Configuration;
using QuizballApp.Data;
using QuizballApp.Services;
using Serilog;
using UsersApp.Repositories;

namespace QuizballApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connString = builder.Configuration.GetConnectionString("QuizballConnection");

            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File(
                 "Log/logs.txt",
                 rollingInterval: RollingInterval.Day,
                 outputTemplate: "[{Timestamp: dd-MM-yyyy HH-mm:ss} {SourceContext} {level}] " +
                                 "{Message} {NewLine} {Exception}",
                 retainedFileCountLimit: null,
                 fileSizeLimitBytes: null
                );
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddDbContext<QuizballDbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddScoped<IApplicationService, ApplicationServiceImpl>();
            builder.Services.AddRepositories();

          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}