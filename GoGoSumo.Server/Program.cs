using GoGoSumo.Server.Helpers;
using GoGoSumo.Server.Repositories;
using GoGoSumo.Server.Services;
using System.Text.Json.Serialization;

namespace GoGoSumo.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddControllers()
            .AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. UserRole)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<DataContext>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<IWeddingRepository, WeddingRepository>();
        builder.Services.AddScoped<IWeddingService, WeddingService>();

        if (builder.Environment.IsDevelopment())
        {
            // Global CORS policy
            builder.Services.AddCors(options => options
                .AddDefaultPolicy(policy => policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials() // allow credentials
                )
            );
        }


        var app = builder.Build();
        app.Logger.LogInformation("Starting server...");
        app.Logger.LogInformation("PostgresConnectionString: {PostgresConnectionString}", builder.Configuration.GetConnectionString("PostgresConnection"));

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}
