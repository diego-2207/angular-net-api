using EspecificacionesTecnicas.Api.Models.Request;
using EspecificacionesTecnicas.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace EspecificacionesTecnicas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class EspecificacionTecnicaController : Controller
    {
        private readonly EspecificacionTecnicaService _service;
        public EspecificacionTecnicaController()
        {
            _service = new EspecificacionTecnicaService();
        }
        [Route("listar")]
        [HttpGet]
        public IActionResult Listar()
        {
            return View();
        }
        [Route("crear")]
        [HttpPost]
        public IActionResult CrearEspecificacion([FromBody] EspecificacionTecnica et)
        {
            string codigoET = _service.CrearEspecificacion(et);
            return Created();
        }
        [Route("buscarFormulario")]
        [HttpGet]
        public IActionResult BuscarFormulario([Required(ErrorMessage = "El código de la ET es requerido.")]string codigoET)
        {
            var especificacion = _service.BuscarETFormulario(codigoET);

            if (especificacion == null)
            {
                return NotFound($"No se encontró una especificación con el código {codigoET}.");
            }
            return Ok(especificacion);
        }
        [Route("obtenerMaestros")]
        [HttpGet]
        public IActionResult ObtenerMaestros()
        {
            var maestros = _service.ObtenerMaestros();
            return Ok(maestros);
        }
    }
}
