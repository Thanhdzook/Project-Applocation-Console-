using DAL;
using System;
using Persistence;

namespace BL{
    public class CustomerBL{
        CustomerDAL customerDAL = new CustomerDAL();
        public int? AddCustomer(Customer customer) => customerDAL.AddCustomerDAL(customer);
        public List<Customer> DisplayAllCustomers(int key) => customerDAL.DisplayAllCustomerDAL(key);
        public Customer? DisplayInfoCustomer(string phoneNumber) => customerDAL.GetByPhoneNDAL(phoneNumber);
        public int GetIDCustomer() => customerDAL.GetIDCustomerDAL();
    }
}