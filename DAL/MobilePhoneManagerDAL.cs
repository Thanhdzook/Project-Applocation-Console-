using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class MobilePhoneDAL
    {
        private string? query;
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlCommand command = new MySqlCommand();


        public int? AddPhone(MobilePhone mp)
        {
            int? result = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                command.CommandText = @"Select Max(mobilePhone_id)+1 as mobilePhone_id from mobilephone;";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                        mp.PhoneId = reader.GetInt32("mobilephone_id");
                    }
                    catch
                    {
                        mp.PhoneId = 100;
                    }
                    reader.Close();
                }
                try
                {
                    command.CommandText = @"
            INSERT INTO mobilephone (mobilePhone_id,mobilePhone_name,chip,memory,camera,operatingSystem,weight,pin,warrantyPeriod,price,amount) 
            VALUES 
            (@mobilephone_id,@mobilePhone_name,@chip,@memory,@camera,@operatingSystem,@weight,@pin,@warrantyPeriod,@price,@amount);";
                    command.Parameters.AddWithValue("@mobilephone_id", mp.PhoneId);
                    command.Parameters.AddWithValue("@mobilePhone_name", mp.PhoneName);
                    command.Parameters.AddWithValue("@chip", mp.Chip);
                    command.Parameters.AddWithValue("@memory", mp.Memory);
                    command.Parameters.AddWithValue("@camera", "12 MP");
                    command.Parameters.AddWithValue("@operatingSystem", mp.OperatingSystem);
                    command.Parameters.AddWithValue("@weight", "179,1g");
                    command.Parameters.AddWithValue("@pin", "2000 mAh");
                    command.Parameters.AddWithValue("@warrantyPeriod", "3 years");
                    command.Parameters.AddWithValue("@price", mp.Price);
                    command.Parameters.AddWithValue("@amount", mp.Amount);
                    command.ExecuteNonQuery();
                    result = (int)command.Parameters["@mobilephone_id"].Value;
                }
                catch { }
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

        public List<MobilePhone> DisplayALLMobilePhoneDAL(int key)
        {
            List<MobilePhone>? mp = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                SELECT * FROM mobilephone where mobilePhone_id > "+key+" and mobilePhone_id < "+(key+11)+";";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                mp = new List<MobilePhone>();
                while (reader.Read())
                {
                    mp.Add(GetPhone(reader));
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
            return mp;
        }
        public MobilePhone GetByIdDAL(int ID)
        {
            MobilePhone m = new MobilePhone();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                select * from mobilephone where mobilePhone_id = '" + ID + "';";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    m = GetPhone(reader);
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
            return m;
        }
        public List<MobilePhone> SreachByOpretingSDAL(string name, string value , int key)
        {
            List<MobilePhone>? mp = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                if(key == 1){
                    query = @"
                select * from mobilephone where operatingSystem = '"+ name +"' order by price "+value+";";
                }
                else if(key == 2){
                    query = @"
                select * from mobilephone where operatingSystem = '"+ name +"' ;";
                }
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                mp = new List<MobilePhone>();
                while (reader.Read())
                {
                    mp.Add(GetPhone(reader));
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
            return mp;
        }
        public MobilePhone GetPhone(MySqlDataReader reader)
        {
            MobilePhone phone = new MobilePhone();
            phone.PhoneId = reader.GetInt32("mobilePhone_id");
            phone.OperatingSystem = reader.GetString("operatingSystem");
            phone.PhoneName = reader.GetString("mobilePhone_name");
            phone.Chip = reader.GetString("chip");
            phone.Memory = reader.GetString("memory");
            phone.Camera = reader.GetString("camera");
            phone.Weight = reader.GetString("weight");
            phone.Pin = reader.GetString("pin");
            phone.WarrantyPeriod = reader.GetString("warrantyPeriod");
            phone.Price = reader.GetInt32("price");
            phone.Amount = reader.GetInt32("amount");
            return phone;
            
        }
        public int? CountDAL(){
            int result = 0;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                try
                {
                    using(var cmd = new MySqlCommand("SELECT COUNT(*) FROM mobilephone", connection))
                    {
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch { }
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
    }
}