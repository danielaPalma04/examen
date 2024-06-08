using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace sistema_asistencia
{
    class Conexion
    {
        private MySqlConnection conn;
        private string server = "localhost";
        private string database = "registro";
        private string user = "root";
        private string password = "";
        private string cadenaConexion;

        public Conexion()
        {
            cadenaConexion = $"Server={server}; Database={database}; Uid={user}; Pwd={password};";
            conn = new MySqlConnection(cadenaConexion);
        }
        public MySqlConnection GetConnection()
        {
            return conn;
        }
    }
}
