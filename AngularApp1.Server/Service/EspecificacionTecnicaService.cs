using EspecificacionesTecnicas.Api.DA;
using EspecificacionesTecnicas.Api.Models;
using EspecificacionesTecnicas.Api.Models.Mantenedores;
using EspecificacionesTecnicas.Api.Models.Request;

namespace EspecificacionesTecnicas.Api.Service
{
    public class EspecificacionTecnicaService
    {
        private readonly EspecificacionTecnicaDA _da;
        public EspecificacionTecnicaService()
        {

            _da = new EspecificacionTecnicaDA();
        }

        public List<Maestro> ObtenerMaestros()
        {
            return _da.ObtenerMaestros();
        }
        public string CrearEspecificacion(EspecificacionTecnica especificacion)
        {
            return _da.CrearEspecificacion(especificacion);
        }
        public EspecificacionLectura BuscarETFormulario(string codigoET)
        {
            return _da.BuscarETFormulario(codigoET);
        }
    }
}
