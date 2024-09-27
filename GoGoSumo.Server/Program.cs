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

        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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
        else
        {
            // Allow specific origins
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                  policy =>
                  {
                      policy.WithOrigins(builder.Configuration.GetValue<string[]>("AllowedOrigins")!)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                  });
            });
        }

        var app = builder.Build();
        app.Logger.LogInformation("Starting server...");
        app.Logger.LogInformation(builder.Configuration.GetValue<string[]>("AllowedOrigins")!.ToString());

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI();
        } else {
            app.UseCors(MyAllowSpecificOrigins);
        }

        app.UseAuthorization();


        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}