namespace EspecificacionesTecnicas.Api.Models.Request
{
    public class EspecificacionTecnica
    {
        public string CodigoET { get; set; } = string.Empty;
        public string Planta { get; set; } = string.Empty;
        public int CodigoMovex { get; set; }
        public string VersionET { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public string NombreProductoRotulacion { get; set; } = string.Empty;
        public string CodigoInformix { get; set; } = string.Empty;
        public int Destino { get; set; }
        public int Cliente { get; set; }
        public int Marca { get; set; }
        public int LineaProducto { get; set; }
        public int EstadoProducto { get; set; }
        public string VidaUtil { get; set; } = string.Empty;
        public int TipoPeso { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int ContenidoMarinado { get; set; }
        public int ClasificacionProducto { get; set; }
        public string RequisitosGenerales { get; set; } = string.Empty;
        public int NormativaInterna { get; set; }
        public string RequisitosCliente { get; set; } = string.Empty;
        public string DocumentacionComplementaria { get; set; } = string.Empty;
        public string ObservacionesGenerales { get; set; } = string.Empty;
        public int Estado { get; set; }
        public string Autor { get; set; } = string.Empty;
    }
}
