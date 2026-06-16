using EspecificacionesTecnicas.Api.Utils;
using Microsoft.Data.SqlClient;

namespace EspecificacionesTecnicas.Api.DA
{
    public class ConexionDB
    {
        private readonly string _connectionString = string.Empty;

        public ConexionDB()
        {
            _connectionString = Config.Configuration.GetConnectionString("SistemaET");
        }

        public SqlConnection GetConnection()
        {
            if(_connectionString == string.Empty) 
                throw new InvalidOperationException("La cadena de conexión no está configurada.");
            
            var con =  new SqlConnection(_connectionString);
            con.Open();
            return con;
        }
    }
}
