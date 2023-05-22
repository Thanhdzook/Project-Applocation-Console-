namespace Persistence
{
    
    public class OrderDetalis
    {
        public Order OrderID {set;get;}
        public long total_price {set ; get;}
        public MobilePhone MobilePhoneOrder {set;get;}
        public int quantity {set;get;}
        public OrderDetalis()
        {
            OrderID = new Order();
        }
    }
}
