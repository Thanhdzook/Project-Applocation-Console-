namespace Persistence
{
    
    public class Order
    {
        public int OrderID {set;get;}
        public Account OrderAccount{get;set;}
        public Customer OrderCustomer {set;get;}
        public MobilePhone MobilePhoneOrder {set;get;}
        public DateTime Order_date {set;get;}
        public int Status {set;get;}
        public Order()
        {
            OrderAccount = new Account();
            OrderCustomer = new Customer();
            MobilePhoneOrder = new MobilePhone();
        }
        // public Account account {set;get;}
    }
}
