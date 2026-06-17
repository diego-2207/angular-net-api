using EspecificacionesTecnicas.Api.Models.Request;
namespace EspecificacionesTecnicas.Api.Models
{
    public class EspecificacionLectura : EspecificacionTecnica
    {
        public DateTimeOffset FechaCreacion { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
