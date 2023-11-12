using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizBall.Repositories;
using QuizballApp.Authentication;
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
            builder.Configuration.AddJsonFile("appsettings.json");
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

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.IncludeErrorDetails = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = false,
                        SignatureValidator = (token, validator) => { return new JwtSecurityToken(token); }
                    };
                });

            builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddDbContext<QuizballDbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddRepositories();
            builder.Services.AddScoped<IAuth, Auth>();

          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}