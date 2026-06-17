using EspecificacionesTecnicas.Api.Models.Mantenedores;
using System.Data;
namespace EspecificacionesTecnicas.Api.DA
{
    public class EspecificacionTecnicaDA
    {
        private readonly ConexionDB _conexion;

        public EspecificacionTecnicaDA()
        {
            _conexion = new ConexionDB(); // Inyectado automáticamente
        }

        public List<Maestro> ObtenerMaestros() { 
            using var connection = _conexion.GetConnection();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "[SP_ET_OBTENER_TABLAS_AUXILIARES]";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                using var reader = cmd.ExecuteReader();
                List<Maestro> maestros = new List<Maestro>();
                do
                {
                    while (reader.Read())
                    {
                        maestros.Add(new Maestro
                        {
                            Id = reader["Id"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Tipo = reader["Tipo"].ToString()
                        });
                    }
                } while (reader.NextResult());
                return maestros;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
