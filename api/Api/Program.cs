using Api;
using Api.Middlewares;
using Infrastructure;
using Application;
using Swashbuckle.AspNetCore.SwaggerUI;
using ConfigureServices = Api.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration, builder.Environment);
builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddControllers();
builder.Logging.ClearProviders();

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

await InitializeDatabaseAsync(app);

if (app.Environment.IsProduction() == false)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
        options.DocExpansion(DocExpansion.None);
    });
}

//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseStaticFiles();

if (app.Environment.IsProduction())
    app.UseCors(ConfigureServices.ProductionCorsPolicy);
else
    app.UseCors(ConfigureServices.DevelopmentCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

// do not touch options parameter please
app.UseExceptionHandler(options => { });

app.MapControllers();

app.Run();

static async Task InitializeDatabaseAsync(IApplicationBuilder app)
{
    await using var scope = app.ApplicationServices.CreateAsyncScope();

    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

    await initializer.CreateDatabaseAsync();
}

public partial class Program { }