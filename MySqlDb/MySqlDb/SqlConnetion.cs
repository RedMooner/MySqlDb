using System;
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
        private MySqlConnection _connection;

        public string Password { get => _password; set => _password = value; }
        public string Username { get => _username; set => _username = value; }
        public string Host { get => _host; set => _host = value; }
        public string Database { get => _database; set => _database = value; }


        public SqlConnetion(string password, string username, string host, string database)
        {
            Password = password;
            Username = username;
            Host = host;
            Database = database;
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
            string connStr = $"server={_host};user={_username};database={_database};password={_password};";
            MySqlConnection conn = new MySqlConnection(connStr);
            return conn;
        }
    }

    public class Query
    {
        private SqlConnetion _connetion;
        public SqlConnetion Connetion { get => _connetion; set => _connetion = value; }

        public Query(SqlConnetion connetion)
        {
            Connetion = connetion;
        }

        public void SELECT(string table)
        {
            _connetion.Open();
            string sql = $"SELECT * FROM {table}";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, _connetion.GetConnection());
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
            }
            reader.Close(); // закрываем reader
                            // закрываем соединение с БД
            _connetion.Close();
        }
    }
}
