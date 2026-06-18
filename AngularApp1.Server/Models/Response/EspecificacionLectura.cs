using EspecificacionesTecnicas.Api.Models.Request;
namespace EspecificacionesTecnicas.Api.Models.Response
{
    public class EspecificacionLectura : EspecificacionCreacion
    {
        public DateTimeOffset FechaCreacion { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
