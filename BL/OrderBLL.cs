using DAL;
using System;
using Persistence;

namespace BL{
    public class OderBL{
        OrderDAL orderDAL = new OrderDAL();
        public int CreateOrder(Order order) => orderDAL.CreateOrderDAL(order);
        public List<Order> DisplayOder(int key) => orderDAL.DisplayAllOdersDAL(key);
        public List<OrderDetalis> DisplayAllOdersDetails(int key , int key2) => orderDAL.DisplayAllOdersDetailsDAL(key, key2);
        public bool ChangeStatus(int status , int id ) => orderDAL.ChangeStatusDAL(status ,id);
        public int? Count() => orderDAL.CountDAL();
        public int? CountDetails() => orderDAL.CountDetailsDAL();
        public Order DisplayOrderById(int id) => orderDAL.GetByID(id);
        public OrderDetalis DisplayOrderDetalisById(int id , int idP) => orderDAL.GetDetailsByID(id , idP);
        public bool InsertOrderDetails(List<Order> orders) => orderDAL.InsertOrderDetailsDAL(orders);
        public int? CountIdCustomer(int id) => orderDAL.CountIdCustomerDAL(id);
        public int GetIdByCustomer(int id) => orderDAL.GetIdByCustomerDAL(id);
        public void DeleteOrder(int id , int idP) => orderDAL.DeleteOrderDAL(id , idP);
    }
}