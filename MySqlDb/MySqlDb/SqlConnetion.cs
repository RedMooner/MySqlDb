using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace MySqlDb
{
    public class SqlConnetion
    {
        private string _database = "db";
        private string _host = "127.0.0.1";
        private string _username = "username";
        private string _password = "password";
        private string _port = "3306";
        private MySqlConnection _connection;

        public string Password { get => _password; set => _password = value; }
        public string Username { get => _username; set => _username = value; }
        public string Host { get => _host; set => _host = value; }
        public string Database { get => _database; set => _database = value; }
        public string Port { get => _port; set => _port = value; }

        public SqlConnetion(string password, string username, string host, string database, string port)
        {
            Password = password;
            Username = username;
            Host = host;
            Database = database;
            Port = port;
            _connection = CreateConnection();
        }
        public void Open()
        {
            _connection.Open();
        }
        public void Close()
        {
            _connection.Close();
        }
        public MySqlConnection GetConnection()
        {
            return _connection;
        }
        private MySqlConnection CreateConnection()
        {
            string connStr = "";
            connStr = $"server={Host};port={Port};uid={Username};pwd={Password};database={Database}";
            MySqlConnection conn = new MySqlConnection(connStr);
            return conn;
        }
    }
}
