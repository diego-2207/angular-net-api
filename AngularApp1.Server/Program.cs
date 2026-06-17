using EspecificacionesTecnicas.Api.Handlers;
using EspecificacionesTecnicas.Api.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//PipeLine para Serilog.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt", 
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

//Añade appsettings a clase de Configuracion.
Config.Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        var scheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            Description = "Ingrese usuario y contraseña (se enviará en Base64)"
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["BasicAuth"] = scheme
        };

        return Task.CompletedTask;
    });
});
//Autenticacion
const string schemeName = "Basic";

builder.Services.AddAuthentication(schemeName)
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(schemeName, null);

builder.Services.AddAuthorization();
// Configuración para manejo global de excepciones
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
// Configuración para manejo global de excepciones
app.UseExceptionHandler();
#if DEBUG
string openApiRoutePattern = "/docs/openapi";
app.MapOpenApi(openApiRoutePattern);
//Configuración para SCALAR
app.MapScalarApiReference("/docs", options => {
    options.OpenApiRoutePattern = openApiRoutePattern;
    options.AddPreferredSecuritySchemes("BasicAuth")
    .AddHttpAuthentication("BasicAuth", auth =>
    {
        auth.Username = "diegoM";
        auth.Password = "123.casa";
    });
});
#endif
app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();