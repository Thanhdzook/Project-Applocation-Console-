using System;
using BL;
using Persistence;
using static System.Console;
using System.Globalization;

namespace ConsolePL
{

    public class SellerMenu
    {
        public void DisplaySellerMenu(Account a)
        {


            MobilePhoneBLL mBLL = new MobilePhoneBLL();
            MobilePhone? m = new MobilePhone();
            List<MobilePhone> phones;
            OderBL oBL = new OderBL();
            Order o = new Order();
            OrderDetalis od = new OrderDetalis();
            List<OrderDetalis> odl = new List<OrderDetalis>();
            CustomerBL cBL = new CustomerBL();
            Customer c = new Customer();
            List<Order> orders = new List<Order>();
            string[] options = { "Show List Mobile Phone", "Show List Order", "Exit" };
            string title = "[Seller Menu]";
            string line = "\t\t\t-----------------------------------------------------------------------------------------------------------";
            string line2 = "\t\t\t\t--------------------------------------------------------------------";
            string line1 = "------------------------------------------------------------------------------";
            Keyboard keyboard = new Keyboard(title, options);
            int SelectedIndex;
            do
            {
                SelectedIndex = keyboard.Run();
                switch (SelectedIndex)
                {
                    case 0:
                        int countId = 0;
                        int count = 1;
                        int? CountPhone = 0;
                        ConsoleKey keyPressed;
                        do
                        {
                            Console.Clear();
                            phones = mBLL.DisplayAllPhone(countId);
                            Utility.DisplayTableMobiPhone("Mobile Phone Infomation", phones, 0);
                            if (mBLL.CountPhone() % 10 != 0)
                            {
                                CountPhone = mBLL.CountPhone() / 10 + 1;
                            }
                            else
                            {
                                CountPhone = mBLL.CountPhone() / 10;
                            }
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\t\t\t\t\t\t\t<   [{0}/{1}]   >", count, (CountPhone));
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\t ● Press 'LEFT' or 'RIGHT' arrow to switch page.");
                            Console.WriteLine("\t\t\t ● Press 'S' to search mobile phone, 'D' to view mobile phone details, 'O' to create Order, 'Esc' to exit.");
                            ConsoleKeyInfo keyInfo = ReadKey(true);
                            keyPressed = keyInfo.Key;
                            if (keyPressed == ConsoleKey.RightArrow)
                            {
                                countId = countId + 10;
                                count++;
                                if (count > CountPhone)
                                {
                                    countId = countId - 10;
                                    count--;
                                }
                            }
                            else if (keyPressed == ConsoleKey.LeftArrow)
                            {
                                countId = countId - 10;
                                count--;
                                if (count == 0)
                                {
                                    countId = countId + 10;
                                    count++;
                                }
                            }
                            else if (keyPressed == ConsoleKey.D)
                            {
                                Console.WriteLine(line);
                                int id = Utility.InputNumber("\t\t\t\t» Enter the mobile phone id: ", 1);
                                m = mBLL.SearchById(id);
                                if (m.PhoneId == null)
                                {
                                    Utility.PrintColor("\t\t\t\tCan't find phone with ID : " + id, 2);

                                }
                                else
                                {
                                    Console.Clear();
                                    Utility.DisplayPhoneByID("Mobile Phone Infomation", m);
                                }
                                Utility.PressAnykey("\t\t\t\tPress any key to exit...");

                            }
                            else if (keyPressed == ConsoleKey.S)
                            {
                                Console.WriteLine(line);
                                ConsoleKey keyPressed1;
                                Console.WriteLine("\t\t\tINSTRUCTION");
                                Console.WriteLine("\t\t\t* Input Operating System: Search by operating system");
                                Console.WriteLine("\t\t\t* Input Operating System # asc|desc: Search by operating system, Price: Low => High or High => Low");
                                Console.WriteLine();
                                Console.Write("\t\t\t\t» Enter value you want to search : ");
                                string name = Console.ReadLine();
                                string[] value = new string[2];
                                value = name.Split(" # ");
                                if (value.Length == 1)
                                {
                                    phones = mBLL.SearchByName(value[0], "", 2);
                                }
                                else if (value[1] != null)
                                {
                                    try
                                    {
                                        phones = mBLL.SearchByName(value[0], value[1], 1);
                                    }
                                    catch
                                    {
                                    }

                                }
                                if (phones.Count != 0)
                                {
                                    int icheck = 1;
                                    do
                                    {
                                        Console.Clear();
                                        Utility.DisplayTableMobiPhone("Mobile Phone Infomation", phones, icheck);
                                        if (phones.Count % 10 != 0)
                                        {
                                            CountPhone = phones.Count / 10 + 1;
                                        }
                                        else
                                        {
                                            CountPhone = phones.Count / 10;
                                        }
                                        Console.WriteLine("\n");
                                        Console.WriteLine("\t\t\t\t\t\t\t\t\t    <   [{0}/{1}]   >", count, (CountPhone));
                                        Console.WriteLine("\n");
                                        Console.WriteLine("\t\t\t\t ● Press 'O' create Order, 'Esc' to exit.");
                                        ConsoleKeyInfo keyInfo1 = ReadKey(true);
                                        keyPressed1 = keyInfo1.Key;
                                        if (keyPressed1 == ConsoleKey.RightArrow)
                                        {
                                            countId = countId + 10;
                                            count++;
                                            icheck = icheck + 5;
                                            if (count > CountPhone)
                                            {
                                                countId = countId - 10;
                                                count--;
                                                icheck = icheck - 5;
                                            }
                                        }
                                        else if (keyPressed1 == ConsoleKey.LeftArrow)
                                        {
                                            countId = countId - 10;
                                            count--;
                                            icheck = icheck - 5;
                                            if (count == 0)
                                            {
                                                countId = countId + 10;
                                                count++;
                                                icheck = icheck + 5;
                                            }
                                        }
                                        if (keyPressed1 == ConsoleKey.O)
                                        {
                                            Console.WriteLine(line2);
                                            int id1 = Utility.InputNumber("\t\t\t\t» Enter the mobile phone id you want to buy : ", 1);
                                            m = mBLL.SearchById(id1);
                                            if (m.PhoneId == null)
                                            {
                                                Utility.PrintColor("\t\t\t\tCan't find mobile phone with ID : " + id1, 2);
                                                Console.ReadKey();
                                            }
                                            else if (m.Amount == 0)
                                            {
                                                Utility.PrintColor("\t\t\t\tThe store does''t have enough mobile phone in stock", 2);
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Utility.DisplayOrder("Mobile Phone Information");
                                                Console.WriteLine("\t\t╠═══════╬════════════════════════════════╬═══════════════════════════╬════════════════════════════════╬═════════╬══════════════════════╣");
                                                Console.WriteLine("\t\t║ {0, -5} ║ {1, -30} ║ {2, -25} ║ {3, -30} ║ {4, -7} ║ {5, -20} ║", m.PhoneId, m.PhoneName, m.OperatingSystem, m.Chip, m.Memory, Utility.Money(m.Price));
                                                Console.WriteLine("\t\t╚═══════╩════════════════════════════════╩═══════════════════════════╩════════════════════════════════╩═════════╩══════════════════════╝");
                                                while (true)
                                                {
                                                    Console.Write("\t\t\t\tDo you want to buy this phone? (Y?N) : ");
                                                    string choose = Console.ReadLine() ?? "";
                                                    if (choose == "y" || choose == "Y")
                                                    {
                                                        Console.WriteLine(line2);
                                                        string phoneNumber = Utility.InputPhoneN("\t\t\t\tEnter buyer's phone number : ", 1);
                                                        try
                                                        {
                                                            c = cBL.DisplayInfoCustomer(phoneNumber);
                                                            Console.WriteLine("\t\t\t\tInfo Customer");
                                                            Console.WriteLine("\t\t\t\tCustomer Name : " + c.CustomerName);
                                                            Console.WriteLine("\t\t\t\tCustomer Address : " + c.CustomerAddress);
                                                            Console.WriteLine(line2);
                                                            int quantity = Utility.InputNumber("\t\t\t\tInput quantity : ", 1);
                                                            if (quantity > m.Amount)
                                                            {
                                                                Utility.PrintColor("\t\t\t\tThe store does't have enough mobile phone in stock !", 2);
                                                                Utility.PressAnykey("\t\t\t\tPress any key to continue...");
                                                                break;
                                                            }
                                                            m.Amount = quantity;
                                                            int.TryParse(Console.ReadLine(), out quantity);
                                                            if (oBL.CountIdCustomer(c.CustomerId) == 0)
                                                            {
                                                                oBL.CreateOrder(o);
                                                                o = new Order { OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                            }
                                                            else
                                                            {
                                                                o = new Order { OrderID = oBL.GetIdByCustomer(c.CustomerId), OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                            }
                                                            orders = new List<Order>();
                                                            orders.Add(o);
                                                            bool i = oBL.InsertOrderDetails(orders);
                                                            if (i == true)
                                                            {
                                                                Utility.PrintColor("\t\t\t\tCreate Order completed!", 1);
                                                            }
                                                            else
                                                            {
                                                                Utility.PrintColor("\t\t\t\tCreate Order not complete!", 2);
                                                            }

                                                            Utility.PressAnykey("\t\t\t\tPress any key to exit...");
                                                            break;
                                                        }
                                                        catch
                                                        {
                                                            string name1 = Utility.InputName("\t\t\t\tEnter customer name : ", 1);
                                                            string address = Utility.InputString("\t\t\t\tEnter customer address : ", 1);
                                                            c = new Customer { CustomerName = name1, PhoneNumber = phoneNumber, CustomerAddress = address };
                                                            cBL.AddCustomer(c);
                                                            c.CustomerId = cBL.GetIDCustomer();
                                                            Console.WriteLine(line2);
                                                            int quantity = Utility.InputNumber("\t\t\t\tInput quantity : ", 1);
                                                            if (quantity > m.Amount)
                                                            {
                                                                Utility.PrintColor("\t\t\t\tThe store does't have enough mobile phone in stock !", 2);
                                                                Utility.PressAnykey("Press any key to continue...");
                                                                break;
                                                            }
                                                            m.Amount = quantity;
                                                            int.TryParse(Console.ReadLine(), out quantity);

                                                            if (oBL.CountIdCustomer(c.CustomerId) == 0)
                                                            {
                                                                o = new Order { OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                                oBL.CreateOrder(o);
                                                            }
                                                            else
                                                            {
                                                                o = new Order { OrderID = oBL.GetIdByCustomer(c.CustomerId), OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                            }
                                                            orders = new List<Order>();
                                                            orders.Add(o);
                                                            bool i = oBL.InsertOrderDetails(orders);
                                                            if (i == true)
                                                            {
                                                                Utility.PrintColor("\t\t\t\tCreate Order completed!", 1);
                                                            }
                                                            else
                                                            {
                                                                Utility.PrintColor("\t\t\t\tCreate Order not complete!", 2);
                                                            }
                                                            Utility.PressAnykey("\t\t\t\tPress any key to exit...");
                                                            break;
                                                        }
                                                    }
                                                    else if (choose == "n" || choose == "N")
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Utility.PrintColor("\t\t\t\tInvaild Input ! Re-Enter.", 2);
                                                        Console.ReadKey();
                                                    }
                                                }
                                            }
                                        }
                                    } while (keyPressed1 != ConsoleKey.Escape);
                                }
                                else
                                {
                                    Utility.PrintColor("\t\t\t\tCan't find mobile phone with value : " + name, 2);
                                    Utility.PressAnykey("\t\t\t\tPress any key to go back...");
                                }
                            }
                            else if (keyPressed == ConsoleKey.O)
                            {
                                Console.WriteLine(line);
                                int id1 = Utility.InputNumber("\t\t\t\t» Enter the mobile phone id you want to buy : ", 1);
                                m = mBLL.SearchById(id1);
                                if (m.PhoneId == null)
                                {
                                    Utility.PrintColor("\t\t\t\tCan't find mobile phone with ID : " + id1, 2);
                                    Console.ReadKey();
                                }
                                else if (m.Amount == 0)
                                {
                                    Utility.PrintColor("\t\t\t\tThe store does''t have enough mobile phone in stock", 2);
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Utility.DisplayOrder("Mobile Phone Information");
                                    Console.WriteLine("\t\t╠═══════╬════════════════════════════════╬═══════════════════════════╬════════════════════════════════╬═════════╬══════════════════════╣");
                                    Console.WriteLine("\t\t║ {0, -5} ║ {1, -30} ║ {2, -25} ║ {3, -30} ║ {4, -7} ║ {5, -20} ║", m.PhoneId, m.PhoneName, m.OperatingSystem, m.Chip, m.Memory, Utility.Money(m.Price));
                                    Console.WriteLine("\t\t╚═══════╩════════════════════════════════╩═══════════════════════════╩════════════════════════════════╩═════════╩══════════════════════╝");
                                    while (true)
                                    {
                                        Console.Write("\t\t\t\tDo you want to buy this phone? (Y?N) : ");
                                        string choose = Console.ReadLine() ?? "";
                                        if (choose == "y" || choose == "Y")
                                        {
                                            Console.WriteLine(line2);
                                            string phoneNumber = Utility.InputPhoneN("\t\t\t\tEnter buyer's phone number : ", 1);
                                            try
                                            {
                                                c = cBL.DisplayInfoCustomer(phoneNumber);
                                                Console.WriteLine("\t\t\t\tInfo Customer");
                                                Console.WriteLine("\t\t\t\tCustomer Name : " + c.CustomerName);
                                                Console.WriteLine("\t\t\t\tCustomer Address : " + c.CustomerAddress);
                                                Console.WriteLine(line2);
                                                int quantity = Utility.InputNumber("\t\t\t\tInput quantity : ", 1);
                                                if (quantity > m.Amount)
                                                {
                                                    Utility.PrintColor("\t\t\t\tThe store does't have enough mobile phone in stock !", 2);
                                                    Utility.PressAnykey("\t\t\t\tPress any key to continue...");
                                                    break;
                                                }
                                                m.Amount = quantity;
                                                int.TryParse(Console.ReadLine(), out quantity);
                                                if (oBL.CountIdCustomer(c.CustomerId) == 0)
                                                {
                                                    o = new Order { OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                    oBL.CreateOrder(o);
                                                }
                                                else
                                                {
                                                    o = new Order { OrderID = oBL.GetIdByCustomer(c.CustomerId), OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                }
                                                orders = new List<Order>();
                                                orders.Add(o);
                                                bool i = oBL.InsertOrderDetails(orders);
                                                if (i == true)
                                                {
                                                    Utility.PrintColor("\t\t\t\tCreate Order completed!", 1);
                                                }
                                                else
                                                {
                                                    Utility.PrintColor("\t\t\t\tCreate Order not complete!", 2);
                                                }
                                                Utility.PressAnykey("\t\t\t\tPress any key to exit...");
                                                break;
                                            }
                                            catch
                                            {
                                                string name1 = Utility.InputName("\t\t\t\tEnter customer name : ", 1);
                                                string address = Utility.InputString("\t\t\t\tEnter customer address : ", 1);
                                                c = new Customer { CustomerName = name1, PhoneNumber = phoneNumber, CustomerAddress = address };
                                                cBL.AddCustomer(c);
                                                c.CustomerId = cBL.GetIDCustomer();
                                                Console.ReadLine();
                                                Console.WriteLine(line2);
                                                int quantity = Utility.InputNumber("\t\t\t\tInput quantity : ", 1);
                                                if (quantity > m.Amount)
                                                {
                                                    Utility.PrintColor("\t\t\t\tThe store does't have enough mobile phone in stock !", 2);
                                                    Utility.PressAnykey("Press any key to continue...");
                                                    break;
                                                }
                                                m.Amount = quantity;
                                                int.TryParse(Console.ReadLine(), out quantity);
                                                if (oBL.CountIdCustomer(c.CustomerId) == 0)
                                                {
                                                    o = new Order { OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                    oBL.CreateOrder(o);
                                                }
                                                else
                                                {
                                                    o = new Order { OrderID = oBL.GetIdByCustomer(c.CustomerId), OrderCustomer = c, OrderAccount = a, MobilePhoneOrder = m };
                                                }
                                                orders = new List<Order>();
                                                orders.Add(o);
                                                bool i = oBL.InsertOrderDetails(orders);
                                                if (i == true)
                                                {
                                                    Utility.PrintColor("\t\t\t\tCreate Order completed!", 1);
                                                }
                                                else
                                                {
                                                    Utility.PrintColor("\t\t\t\tCreate Order not complete!", 2);
                                                }
                                                Utility.PressAnykey("\t\t\t\tPress any key to exit...");
                                                break;
                                            }
                                        }
                                        else if (choose == "n" || choose == "N")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Utility.PrintColor("\t\t\t\tInvaild Input ! Re-Enter.", 2);
                                            Console.ReadKey();
                                        }
                                    }
                                }
                            }
                        } while (keyPressed != ConsoleKey.Escape);
                        break;
                    case 1:
                        int countOrderId = 0;
                        int count1 = 1;
                        int? CountOrder = 0;
                        ConsoleKey keyPressed3;
                        do
                        {
                            Console.Clear();
                            odl = oBL.DisplayAllOdersDetails(countOrderId, 0);
                            Utility.DisplayTableOrderDetails("Order Details", odl);
                            if (oBL.CountDetails() % 10 != 0)
                            {
                                CountOrder = oBL.CountDetails() / 10 + 1;
                            }
                            else
                            {
                                CountOrder = oBL.CountDetails() / 10;
                            }
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\t\t\t\t\t\t\t    <   [{0}/{1}]   >", count1, (CountOrder));
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\t\t ● Press 'X' to cancel order");
                            Console.WriteLine("\t\t\t\t ● Press 'Esc' to exit.");
                            ConsoleKeyInfo keyInfo3 = ReadKey(true);
                            keyPressed3 = keyInfo3.Key;
                            if (keyPressed3 == ConsoleKey.RightArrow)
                            {
                                countOrderId = countOrderId + 10;
                                count1++;
                                if (count1 > CountOrder)
                                {
                                    countOrderId = countOrderId - 10;
                                    count1--;
                                }
                            }
                            else if (keyPressed3 == ConsoleKey.LeftArrow)
                            {
                                countOrderId = countOrderId - 10;
                                count1--;
                                if (count1 == 0)
                                {
                                    countOrderId = countOrderId + 10;
                                    count1++;
                                }
                            }
                            else if (keyPressed3 == ConsoleKey.X)
                            {
                                Console.WriteLine(line2);
                                int id = Utility.InputNumber("\t\t\t\t» Enter the order id you want to cancel : ", 1);
                                o = oBL.DisplayOrderById(id);
                                if (o.OrderID == 0)
                                {
                                    Utility.PrintColor("\t\t\t\tCan't find mobile phone with ID : " + id, 2);
                                    Console.ReadKey();
                                }
                                else
                                {
                                    int idP = Utility.InputNumber("\t\t\t\t» Enter mID to cancel : ", 1);
                                    od = oBL.DisplayOrderDetalisById(id, idP);
                                    try
                                    {
                                        int? id1 = od.MobilePhoneOrder.PhoneId;
                                        oBL.DeleteOrder(id, idP);
                                        Utility.PrintColor("\t\t\t\tCancel Order successful !",1);
                                        Console.ReadKey();
                                    }
                                    catch
                                    {
                                        Utility.PrintColor("\t\t\t\tCan't find mobile phone with ID : " + idP, 2);
                                        Console.ReadKey();
                                    }
                                }
                            }
                        } while (keyPressed3 != ConsoleKey.Escape);
                        break;
                }
            } while (SelectedIndex != 2);
            Environment.Exit(0);
        }
    }
}