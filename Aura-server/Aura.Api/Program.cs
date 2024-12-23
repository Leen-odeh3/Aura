using Aura.Api.Middleware;
using Aura.Application;
using Aura.Infrastructure;
namespace Aura.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.addDependancy(builder.Configuration)
            .addApplicationDependancy().AddPresentationDependencies(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        builder.Services.AddCors(c =>
        {
            c.AddDefaultPolicy(options =>
            options.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
        });

        app.UseCors();

        app.UseHttpsRedirection();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();


        app.Run();
    }
}
