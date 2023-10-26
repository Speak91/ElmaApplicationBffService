using ElmaApplicationBffService.Core.Options;
using ElmaApplicationBffService.Core;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using ElmaApplicationBffService.Core.Infrastructure;
using ElmaApplicationBffService.Core.Requests;
using System.Reflection;

namespace ElmaApplicationBffService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;
            var env = builder.Environment;
            
            // Add services to the container.
            services.AddOptions();
            services.Configure<ElmaOptions>(configuration.GetSection("Elma"));
            services.AddControllers();
            services.AddCoreServices();
            services.AddRestClients(configuration);
            services.AddScoped<ApiExceptionFilterAttribute>();
            services.AddValidatorsFromAssembly(typeof(CoreServicesExtensions).Assembly);
            services.AddFluentValidationRulesToSwagger();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}