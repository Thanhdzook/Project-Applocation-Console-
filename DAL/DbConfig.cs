using MySql.Data.MySqlClient;
using System.Text.Json;
namespace DAL;
public class DbConfig
{
    // private static MySqlConnection connection = new MySqlConnection();
        public string server { set; get; }
        public string userId { set; get; }
        public string password { set; get; }
        public string port { get; set; }
        public string database { set; get; }
        public DbConfig()
        {
            server = "localhost";
            userId = "root";
            password = "Ttadhp2608.";
            port = "3306";
            database =  "mobilephoneshop";
        }
        public static MySqlConnection GetDefaultConnection() => GetConnection(new DbConfig());
        public static MySqlConnection GetConnection()
        {
            MySqlConnection connection = GetDefaultConnection();
            try
            {
                string json = File.ReadAllText("DbConfig.json");
                DbConfig dbConfig = JsonSerializer.Deserialize<DbConfig>(json)!;
                connection = GetConnection(dbConfig);
            }
            catch
            { }
            return connection;
        }
        public static MySqlConnection GetConnection(DbConfig db)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = $"server={db.server};userid={db.userId};password={db.password};port={db.port};database={db.database}";
            return connection;
        }

    
}