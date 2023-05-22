using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class AccountDAL
    {
        private string? query;
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlCommand command = new MySqlCommand();

        // kết nối cách 1
        public Account LoginDAL(string username, string password)
        {
            Account account = new Account();
            try
            {
                connection.Open();
                query = @"select * from account where userName = '" + username + "' and password = '" + password + "';";
                MySqlDataReader reader = (new MySqlCommand(query, connection)).ExecuteReader();
                if (reader.Read())
                {
                    account = GetByAccount(reader);
                }
                reader.Close();
            }
            catch { 

            }
            finally
            {
                connection.Close();
            }
            return account;
        }
        
        public Account GetByID(int id)
        {
            Account acc = new Account();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                SELECT * FROM account where account_id = '"+id+"';";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    acc = GetByAccount(reader);
                }
                reader.Close();

            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return acc;
        }        
        public Account GetByAccount(MySqlDataReader reader)
        {
            Account account = new Account();
            account.AccountId = reader.GetInt32("account_id");
            account.Name = reader.GetString("name");
            account.Username = reader.GetString("userName");
            account.Password = reader.GetString("password");
            account.PhoneN = reader.GetString("phoneN");
            account.AccountRole = reader.GetInt32("role");
            return account;
        }
    }
}