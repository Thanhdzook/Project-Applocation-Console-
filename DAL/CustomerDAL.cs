using System;
using Persistence;
using MySql.Data.MySqlClient;

namespace DAL{
    public class CustomerDAL{
        private string? query;
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlCommand command = new MySqlCommand();
        public int? AddCustomerDAL(Customer customer){
            int? result = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                command.CommandText = @"Select Max(customer_id)+1 as customer_id from customer;";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                        customer.CustomerId = reader.GetInt32("customer_id");
                    }
                    catch
                    {
                        customer.CustomerId = 1;
                    }
                    reader.Close();
                }
                try
                {
                    command.CommandText = @"
            INSERT INTO customer (customer_id,customer_name,customer_phonenumber,customer_address) 
            VALUES 
            (@customer_id,@customer_name,@customer_phonenumber,@customer_address);";
                    command.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                    command.Parameters.AddWithValue("@customer_name", customer.CustomerName);
                    command.Parameters.AddWithValue("@customer_phonenumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@customer_address", customer.CustomerAddress);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    result = (int)command.Parameters["@customer_id"].Value;
                }
                catch {
                    result = -1;
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public List<Customer> DisplayAllCustomerDAL(int key){
            List<Customer>? c = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                SELECT * FROM customer where customer_id > "+key+" and customer_id < "+(key+11)+";";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                c = new List<Customer>();
                while (reader.Read())
                {
                    c.Add(GetCustomer(reader));
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
            return c;
        }
        public Customer? GetByPhoneNDAL(string phoneNumer){
            Customer? customer = null;
            try
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    command.Connection = connection;
                    query = @"
                    select * from customer where customer_phonenumber = '"+phoneNumer+"';";
                    command.CommandText = query;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        customer = GetCustomer(reader);
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
            return customer;
        }
        public Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.CustomerId = reader.GetInt32("customer_id");
            customer.CustomerName = reader.GetString("customer_name");
            customer.PhoneNumber = reader.GetString("customer_phonenumber");
            customer.CustomerAddress = reader.GetString("customer_address");
            return customer;
        }
        public Customer GetByID(int ID)
        {
            Customer c = new Customer();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                select * from customer where customer_id = '" + ID + "';";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    c = GetCustomer(reader);
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
            return c;
        }
        public int GetIDCustomerDAL(){
            int id = 0;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                select customer_id from customer order by customer_id desc limit 1;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32("customer_id");
                }
                reader.Close();
                return id;
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
            return id;
        }
    }
}