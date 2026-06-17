using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EspecificacionesTecnicas.Api.Handlers
{
    /*Esta clase implementa al interfaz de .Net IExceptionHandler y se va a encargar
      de procesar todas las excepciones a nivel global en la API.
    Es necesario que esté registrada en el Program.cs para funcionar.
    */
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //Esto dispara el log de Serilog
            _logger.LogError(exception, "Ocurrió una excepción no controlada: {Message}", exception.Message);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            var detallesError = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error Interno del Servidor",
                Detail = "Ocurrió un error inesperado. Comuníquese con soporte si el problema persiste.",
                Instance = httpContext.Request.Path
            };

            await httpContext.Response.WriteAsJsonAsync(detallesError, cancellationToken);

            return true;
        }

    }
}
