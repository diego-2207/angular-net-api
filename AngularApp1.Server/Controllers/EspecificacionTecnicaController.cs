using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EspecificacionesTecnicas.Api.Models;
using EspecificacionesTecnicas.Api.DA;
namespace EspecificacionesTecnicas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class EspecificacionTecnicaController : Controller
    {
        private readonly EspecificacionTecnicaDA _especificacionTecnicaDA;
        public EspecificacionTecnicaController()
        {
            _especificacionTecnicaDA = new EspecificacionTecnicaDA();
        }
        [Route("listar")]
        [HttpGet]
        public IActionResult Listar()
        {
            return View();
        }
        [Route("crear")]
        [HttpPost]
        public IActionResult CrearEspecificacion([FromBody] EspecificacionTecnica especificacion)
        {  
            var respuesta = new RespuestaApi
            {
                Estado = "Ok",
                Mensaje = "Creación exitosa."
            };
            return Ok(respuesta);
        }
        [Route("obtenerMaestros")]
        [HttpGet]
        public IActionResult ObtenerMaestros()
        {
            throw new Exception("Error al obtener maestros."); // Simulación de error para probar manejo global de excepciones
            var maestros = _especificacionTecnicaDA.ObtenerMaestros();
            return Ok(maestros);
        }
    }
}
