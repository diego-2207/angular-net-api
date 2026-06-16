using EspecificacionesTecnicas.Api.Models.Mantenedores;

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
            try
            {
                using var reader = cmd.ExecuteReader();
                //for (int i = 0; i < reader.count; i++)
                //{

                //}
                while (reader.Read())
                {

                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
