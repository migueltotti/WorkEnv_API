using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using WorkEnv.API.ExceptionHandler;
using WorkEnv.CrossCutting.DependencyInjection;

namespace WorkEnv.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        
        builder.Services.AddCors( options =>
        {
            options.AddPolicy("EnableCors", police =>
            {
                police.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build();
            });
        });
        
        builder.Services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
                RateLimitPartition.GetTokenBucketLimiter(httpcontext.User.Identity?.Name ??
                                                         httpcontext.Request.Headers.Host.ToString(),
                    partition => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 30,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(5),
                        TokensPerPeriod = 26,
                        AutoReplenishment = true,
                        QueueLimit = 0,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    }));
        });

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.FullName?.Replace(".", "_")); 
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