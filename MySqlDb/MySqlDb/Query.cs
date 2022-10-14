using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace MySqlDb
{
    public class Query
    {
        private SqlConnetion _connetion;
        public SqlConnetion Connetion { get => _connetion; set => _connetion = value; }

        public Query(SqlConnetion connetion)
        {
            Connetion = connetion;
        }

        public List<string[]> SELECT(string table, int columnCount)
        {
            List<string[]> notes = new List<string[]>();
            _connetion.Open();
            string sql = $"SELECT * FROM {table}";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, _connetion.GetConnection());
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                string[] values = new string[columnCount];
                for (int i = 0; i < columnCount; i++)
                    values[i] = reader.GetValue(i).ToString();
                notes.Add(values);
            }
            reader.Close(); // закрываем reader
            _connetion.Close();                            // закрываем соединение с БД
            return notes;
        }

        public List<string[]> SELECT(string table, string[] columns)
        {
            List<string[]> notes = new List<string[]>();
            _connetion.Open();
            string sql = $"SELECT ";
            var cols = string.Join(",", columns);
            sql += cols;
            sql += " FROM " + table;

            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, _connetion.GetConnection());
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                string[] values = new string[columns.Length];

                for (int i = 0; i < columns.Length; i++)
                {
                    values[i] = reader.GetValue(i).ToString();
                }
                notes.Add(values);
            }
            reader.Close(); // закрываем reader
            _connetion.Close();                            // закрываем соединение с БД
            return notes;
        }
        public void INSERT(string table ,Dictionary<string,string> values)
        {
            string sql = $"INSERT INTO {table} (";
            foreach (var item in values)
                sql += $"{values.Keys} , ";
            sql += ") VALUES(";
            foreach (var item in values)
                sql += $"'{item.Value}',";
            sql += ")";
            _connetion.Open();
            MySqlCommand command = new MySqlCommand(sql, _connetion.GetConnection());
            command.ExecuteNonQuery();
            _connetion.Close();
        }
    }
}
