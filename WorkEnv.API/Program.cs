using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using WorkEnv.API.ExceptionHandler;
using WorkEnv.CrossCutting.DependencyInjection;
using WorkEnv.Domain.Entities;

namespace WorkEnv.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        if(builder.Environment.IsDevelopment()) DotEnv.Load();

        // Add services to the container.

        builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales", Version = "v1" });

            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            
            //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Bearer JWT "
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{ }
                }
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseExceptionHandler();

        app.UseRateLimiter();
        app.UseCors("EnableCors");
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}