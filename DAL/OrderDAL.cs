using System;
using Persistence;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class OrderDAL
    {
        private string? query;
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlCommand command = new MySqlCommand();
        public int CreateOrderDAL(Order order)
        {
            int result = -1;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                command.CommandText = @"Select Max(order_id)+1 as order_id from orders;";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                        order.OrderID = reader.GetInt32("order_id");
                    }
                    catch
                    {
                        order.OrderID = 1;
                    }
                    reader.Close();
                }
                try
                {
                    command.CommandText = @"
            INSERT INTO orders (order_id,customer_id,seller_id,order_date,status) 
            VALUES 
            (@order_id,@customer_id,@seller_id,@order_date,@status);";
                    command.Parameters.AddWithValue("@order_id", order.OrderID);
                    command.Parameters.AddWithValue("@customer_id", order.OrderCustomer.CustomerId);
                    command.Parameters.AddWithValue("@seller_id", order.OrderAccount.AccountId);
                    command.Parameters.AddWithValue("@order_date", DateTime.Now);
                    command.Parameters.AddWithValue("@status", 1);
                    result = (int)command.Parameters["@order_id"].Value;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine( e.Message );
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
        public List<Order> DisplayAllOdersDAL(int key)
        {
            List<Order>? o = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                SELECT * FROM orders where order_id > " + key + " and order_id < " + (key + 11) + ";";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                o = new List<Order>();
                while (reader.Read())
                {
                    o.Add(GetOrder(reader));
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
            return o;
        }
        public Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderID = reader.GetInt32("order_id");
            order.Status = reader.GetInt32("status");
            order.Order_date = reader.GetDateTime("order_date");
            CustomerDAL customer = new CustomerDAL();
            order.OrderCustomer = customer.GetByID(reader.GetInt32("customer_id"));
            AccountDAL account = new AccountDAL();
            order.OrderAccount = account.GetByID(reader.GetInt32("seller_id"));
            return order;
        }
        public bool ChangeStatusDAL(int status, int id)
        {
            bool result = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                        UPDATE orders SET status = '" + status + "' WHERE (order_id = '" + id + "');";
                command.CommandText = query;
                command.ExecuteNonQuery();
                result = true;

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
        public Order GetByID(int id)
        {
            Order? o = new Order();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                select * from orders where order_id = '" + id + "';";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    o = GetOrder(reader);
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
            return o;
        }
        public bool InsertOrderDetailsDAL(List<Order> lOrder)
        {
            MobilePhoneDAL mPdal = new MobilePhoneDAL();
            MobilePhone m = new MobilePhone();
            bool result = false;
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            command.Connection = connection;
            try
            {
                command.CommandText = "LOCK TABLES account write, customer write, mobilephone write, orders write, orderdetails write;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;
                try
                {
                    foreach (var i in lOrder)
                    {
                        // get mobilePhone
                        m = mPdal.GetByIdDAL(i.OrderID);
                        decimal price = i.MobilePhoneOrder.Amount * m.Price;
                        command.CommandText = @"INSERT INTO orderdetails(order_id,mobilephone_id,unit_price,quantity ) 
                         VALUE('" + i.OrderID + "','" + i.MobilePhoneOrder.PhoneId + "','" + price + "','" + i.MobilePhoneOrder.Amount + "');";
                        command.ExecuteNonQuery();
                    }
                    //commit transaction 
                    trans.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    try
                    { trans.Rollback(); }
                    catch { }
                }
                finally
                {
                    //unlock all tables; 
                    command.CommandText = "unlock tables;";
                    command.ExecuteNonQuery();
                }
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public int? CountDAL()
        {
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
                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM orders", connection))
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
        public int? CountIdCustomerDAL(int id){
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
                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM orders where customer_id = "+id+" and status = 1", connection))
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
        public int GetIdByCustomerDAL(int id){
            int idorder = 0;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                select * from orders where customer_id = '" + id + "' and status = 1;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idorder = reader.GetInt32("order_id");
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
            return idorder;
        }
        public List<OrderDetalis> DisplayAllOdersDetailsDAL(int key , int key2)
        {
            List<OrderDetalis>? od = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                if(key2 == 0){
                    query = @"
                SELECT * FROM orderdetails where order_id > "+key+" and order_id < "+(key + 11)+"";
                }
                else{
                    query = @"
                SELECT * FROM orderdetails where order_id = "+key+"";
                }
                
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                od = new List<OrderDetalis>();
                while (reader.Read())
                {
                    od.Add(GetOrderDetalis(reader));
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
            return od;
        }
        public OrderDetalis GetOrderDetalis(MySqlDataReader reader)
        {
            OrderDetalis orderd = new OrderDetalis();
            orderd.OrderID.OrderID = reader.GetInt32("order_id");
            orderd.total_price = reader.GetInt32("unit_price");
            MobilePhoneDAL m = new MobilePhoneDAL();
            orderd.MobilePhoneOrder = m.GetByIdDAL(reader.GetInt32("mobilePhone_id"));
            orderd.quantity = reader.GetInt32("quantity");
            return orderd;
        }
        public OrderDetalis GetDetailsByID(int id , int PhoneId)
        {
            OrderDetalis? od = new OrderDetalis();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                select * from orderdetails where order_id = '" + id + "' and mobilePhone_id = "+PhoneId+";";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    od = GetOrderDetalis(reader);
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
            return od;
        }
        public void DeleteOrderDAL(int id , int PhoneId)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;
                query = @"
                delete from orderdetails where order_id = '" + id + "' and mobilePhone_id = "+PhoneId+";";
                command.CommandText = query;
                command.ExecuteReader();
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }
        }
        public int? CountDetailsDAL()
        {
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
                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM orderdetails", connection))
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
