using CourseAPI.Data;
using CourseAPI.DTO;
using CourseAPI.Exceptions;
using CourseAPI.Repository;
using CourseAPI.Services;
using CourseAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CourseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build())
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog();

                builder.Services.AddDbContext<CourseAPIContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CourseAPIContext") ?? throw new InvalidOperationException("Connection string 'CourseAPIContext' not found.")));

                // Add services to the container.
                builder.Services.AddScoped<ICourseRepo, CourseRepo>();
                builder.Services.AddScoped<ICourseService, CourseService>();

                // Register validators
                builder.Services.AddScoped<IValidator<CreateDto>, CreateDTOValidator>();
                builder.Services.AddScoped<IValidator<UpdateDto>, UpdateValidator>();
                builder.Services.AddScoped<IValidator<GetByIDDto>, GetByIDValidator>();
                builder.Services.AddScoped<IValidator<GetAlldto>, GetAllValidator>();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseSerilogRequestLogging();
                app.UseHttpsRedirection();

                app.UseMiddleware<MiddleWareException>();
                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
