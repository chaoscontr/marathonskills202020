using MySql.Data.MySqlClient;

namespace marathonskills
{
    class sql
    {
        public static MySqlConnection 
            GetDBConnection()
        {
            string db = "marathonskills";
            string host = "127.0.0.1";
            string user = "root";
            int port = 3306;
            string connString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Port=" + port + ";";
            variables.conn = new MySqlConnection(connString);
            variables.conn.Open();
            return variables.conn;
        }
    }
}
