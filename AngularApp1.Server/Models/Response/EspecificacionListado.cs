namespace EspecificacionesTecnicas.Api.Models.Response
{
    public class EspecificacionListado
    {
        public string CodigoET { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public DateTimeOffset FechaCreacion { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
    }
}
