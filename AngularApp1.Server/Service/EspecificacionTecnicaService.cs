using EspecificacionesTecnicas.Api.DA;
using EspecificacionesTecnicas.Api.Models.Mantenedores;

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
    }
}
