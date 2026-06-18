using EspecificacionesTecnicas.Api.Models.Mantenedores;
using System.Data;
using EspecificacionesTecnicas.Api.Models.Request;
using Microsoft.AspNetCore.Http.HttpResults;
using EspecificacionesTecnicas.Api.Models.Response;
namespace EspecificacionesTecnicas.Api.DA
{
    public class EspecificacionTecnicaDA
    {
        private readonly ConexionDB _conexion = new ConexionDB();
        public List<Maestro> ObtenerMaestros() { 
            using var connection = _conexion.GetConnection();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "[SP_ET_OBTENER_TABLAS_AUXILIARES]";
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = cmd.ExecuteReader();
            List<Maestro> maestros = new List<Maestro>();
            do
            {
                while (reader.Read())
                {
                    maestros.Add(new Maestro
                    {
                        Id = (string)reader["Id"],
                        Descripcion = (string)reader["Descripcion"],
                        Tipo = (string)reader["Tipo"]
                    });
                }
            } while (reader.NextResult());
            return maestros;
        }

        /// <summary>
        /// Inserta una nueva Especificación Técnica en la base de datos.
        /// </summary>
        /// <param name="et">Objeto que contiene todos los campos necesarios para crear una Especificación Técnica.</param>
        /// <returns>El código de la Especificación Creada.</returns>
        public string CrearEspecificacion(EspecificacionCreacion et)
        {
            using var connection = _conexion.GetConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = "[SP_ET_CREAR]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Codigo_ET",et.CodigoET);
            cmd.Parameters.AddWithValue("@Planta",et.Planta);
            cmd.Parameters.AddWithValue("@Codigo_Movex",et.CodigoMovex);
            cmd.Parameters.AddWithValue("@Fecha_Creacion", DateTimeOffset.UtcNow);
            cmd.Parameters.AddWithValue("@Version_ET",et.VersionET);
            cmd.Parameters.AddWithValue("@Nombre_Producto",et.NombreProducto);
            cmd.Parameters.AddWithValue("@Nombre_Producto_Rotulacion",et.NombreProductoRotulacion);
            cmd.Parameters.AddWithValue("@Codigo_Informix",et.CodigoInformix);
            cmd.Parameters.AddWithValue("@Destino",et.Destino);
            cmd.Parameters.AddWithValue("@Cliente",et.Cliente);
            cmd.Parameters.AddWithValue("@Marca",et.Marca);
            cmd.Parameters.AddWithValue("@Linea_Producto",et.LineaProducto);
            cmd.Parameters.AddWithValue("@Estado_Producto",et.EstadoProducto);
            cmd.Parameters.AddWithValue("@Vida_Util",et.VidaUtil);
            cmd.Parameters.AddWithValue("@Tipo_Peso",et.TipoPeso);
            cmd.Parameters.AddWithValue("@Descripcion",et.Descripcion);
            cmd.Parameters.AddWithValue("@Contenido_Marinado",et.ContenidoMarinado);
            cmd.Parameters.AddWithValue("@Clasificacion_Producto",et.ClasificacionProducto);
            cmd.Parameters.AddWithValue("@Requisitos_Generales",et.RequisitosGenerales);
            cmd.Parameters.AddWithValue("@Normativa_Interna",et.NormativaInterna);
            cmd.Parameters.AddWithValue("@Requisitos_Cliente",et.RequisitosCliente);
            cmd.Parameters.AddWithValue("@Documentacion_Complementaria",et.DocumentacionComplementaria);
            cmd.Parameters.AddWithValue("@Observaciones_Generales",et.ObservacionesGenerales);
            cmd.Parameters.AddWithValue("@Estado",et.Estado);
            cmd.Parameters.AddWithValue("@Autor",et.Autor);

            cmd.ExecuteNonQuery();
            return et.CodigoET;
        }
        
        public EspecificacionLectura? BuscarETFormulario(string codigoET)
        {
            using var connection = _conexion.GetConnection();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "[SP_ET_BUSCAR_FORMULARIO]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Codigo_ET", codigoET);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            var especificacion = new EspecificacionLectura
            {
                CodigoET = codigoET,
                Planta = (string)reader["Planta"],                                  //Casteo directo para campos que no pueden ser nulos.
                CodigoMovex = (int)reader["Codigo_Movex"],                          //Para cammpos que pueden ser nulos: as string o Convert.ToInt32
                FechaCreacion = (DateTimeOffset)reader["Fecha_Creacion"],
                FechaModificacion = reader.IsDBNull("Fecha_Modificacion") 
                                    ? null      //Al pendiente de verificar si lanza una excepcion. en ese caso es necesario castear directo (DatetimeOffset?) null
                                    : (DateTimeOffset)reader["Fecha_Modificacion"],
                VersionET = (string)reader["Version_ET"],
                NombreProducto = (string)reader["Nombre_Producto"],
                NombreProductoRotulacion = (string)reader["Nombre_Producto_Rotulacion"],
                CodigoInformix = (string)reader["Codigo_Informix"],
                Destino = (int)reader["Destino"],
                Cliente = (int)reader["Cliente"],
                Marca = (int)reader["Marca"],
                LineaProducto = (int)reader["Linea_Producto"],
                EstadoProducto = (int)reader["Estado_Producto"],
                VidaUtil = (string)reader["Vida_Util"],
                TipoPeso = (int)reader["Tipo_Peso"],
                Descripcion = (string)reader["Descripcion"],
                ContenidoMarinado = (int)reader["Contenido_Marinado"],
                ClasificacionProducto = (int)reader["Clasificacion_Producto"],
                RequisitosGenerales = (string)reader["Requisitos_Generales"],
                NormativaInterna = (int)reader["Normativa_Interna"],
                RequisitosCliente = (string)reader["Requisitos_Cliente"],
                DocumentacionComplementaria = (string)reader["Documentacion_Complementaria"],
                ObservacionesGenerales = (string)reader["Observaciones_Generales"],
                Estado = (int)reader["Estado"],
                Autor = (string)reader["Autor"],
                Activo = (bool)reader["Activo"]
            };

            return especificacion;
        }
        public List<EspecificacionListado> Listar()
        {
            using var con = _conexion.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "[SP_ET_LISTAR]";
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = cmd.ExecuteReader();
            var listado = new List<EspecificacionListado>();
            while (reader.Read())
            {
                var et = new EspecificacionListado
                {
                    CodigoET = (string)reader["Codigo_ET"],
                    NombreProducto = (string)reader["Nombre_Producto"],
                    FechaCreacion = (DateTimeOffset)reader["Fecha_Creacion"],
                    FechaModificacion = reader.IsDBNull("Fecha_Modificacion")
                                        ? null
                                        : (DateTimeOffset)reader["Fecha_Modificacion"]
                };
                listado.Add(et);
            }

            return listado;
        }
    }
}
