using EspecificacionesTecnicas.Api.DA;
using EspecificacionesTecnicas.Api.Models.Mantenedores;
using EspecificacionesTecnicas.Api.Models.Request;
using EspecificacionesTecnicas.Api.Models.Response;

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
        public string CrearEspecificacion(EspecificacionCreacion especificacion)
        {
            return _da.CrearEspecificacion(especificacion);
        }
        public EspecificacionLectura BuscarETFormulario(string codigoET)
        {
            return _da.BuscarETFormulario(codigoET);
        }
        public List<EspecificacionListado> Listar()
        {
            return _da.Listar();
        }
    }
}
