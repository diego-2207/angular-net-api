using System.Data;

namespace EspecificacionesTecnicas.Api.DA
{
    public class UserDB
    {
        ConexionDB _conexionDB;
        public UserDB()
        {
            _conexionDB = new ConexionDB();
        }
        public string getUserPassword(string usuario)
        {
            using var con = _conexionDB.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SP_API_USUARIO_GET_PASSWORD]";
            cmd.Parameters.AddWithValue("@Nombre_Usuario", usuario);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return reader["CLAVE"].ToString();
            }

            return null;
        }
        public string getUserRol(string usuario)
        {
            using var con = _conexionDB.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SP_API_USUARIO_GET_ROL]";
            cmd.Parameters.AddWithValue("@Nombre_Usuario", usuario);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return reader["ROL"].ToString();
            }
            return null;
        }
    }
}
