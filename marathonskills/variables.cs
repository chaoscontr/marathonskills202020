using System.Data;
using MySql.Data.MySqlClient;

namespace marathonskills
{
    class variables
    {
        public static MySqlConnection conn;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        public static MySqlDataAdapter adapter;
        public static DataTable dt;
        public static DataSet ds;
        public static string sql;
        public static string gCharityDescription;
        public static string gCharityName;
        public static string gCharityImage;
        public static string gMonthCard;
        public static string gYearCard;
        public static string gNameRunner;
        public static int gCharityCash;
        public static string gUserLogin;
        public static string gUserRole;
        public static string gUserId;
        public static string gUserPassword;
        
    }
}
