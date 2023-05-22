using System;
using Persistence;
using System.Text.RegularExpressions;
using System.Globalization;


namespace ConsolePL
{
    public static class Utility
    {
        public static string InputString(string title,int i)
        {
            Console.Write($"{title}");
            string data;
            while (true)
            {
                data = Console.ReadLine() ?? "";
                if (IsVailString(data))
                {
                    break;
                }
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invail Input ! Re-enter : ");
                }
                if (i == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t\t\t\tInvail Input ! Re-enter : ");
                }
            }
            Console.ResetColor();
            return data;
        }
        public static int InputNumber(string title, int i)
        {
            Console.Write($"{title}");
            int number;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out number);
                if (number > 0)
                {
                    break;
                }
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invail Input ! Re-enter : ");
                }
                if (i == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t\t\t\tInvail Input ! Re-enter : ");
                }
            }
            Console.ResetColor();
            return number;
        }
        public static int InputMoney1()
        {
            Console.WriteLine("Price : ");
            int i = (int)InputMoney();
            return i;
        }
        public static string InputPhoneN(string title, int i)
        {
            Console.Write($"{title}");
            string data;
            while (true)
            {
                data = Console.ReadLine() ?? "";
                if (IsVailPhoneN(data))
                {
                    break;
                }
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invail Input ! Re-enter : ");
                }
                if (i == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t\t\t\tInvail Input ! Re-enter : ");
                }

            }
            Console.ResetColor();

            return data;
        }
        public static string InputName(string title, int i)
        {
            Console.Write($"{title}");
            string data;
            while (true)
            {
                data = Console.ReadLine() ?? "";
                if (IsVailName(data))
                {
                    break;
                }
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invail Input ! Re-enter : ");
                }
                if (i == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t\t\t\tInvail Input ! Re-enter : ");
                }
            }
            Console.ResetColor();

            return data;
        }
        public static int MenuListOption(this List<string> selectionList, int index, string title, string prompt)
        {
            string line = "--------------------------------------------------------";
            Console.Clear();
            if (prompt != null)
            {
                Console.WriteLine(line);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(prompt);
            }
            Console.ResetColor();
            Console.WriteLine(line);
            if (title != null)
            {
                Console.WriteLine("\t{0}", title);
            }
            Console.WriteLine("\n");
            ConsoleKeyInfo key;
            bool isLoop = false;
            while (true)
            {
                if (index < 0)
                    index = 0;
                else if (index > selectionList.Count - 1)
                    index = selectionList.Count - 1;
                if (isLoop)
                    Console.CursorTop -= selectionList.Count;
                for (int i = 0; i < selectionList.Count; i++)
                {
                    Console.Write("     ");
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(selectionList[i]);
                    if (i == index)
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                key = Console.ReadKey(true);
                if (char.IsDigit(key.KeyChar))
                    index = Convert.ToInt32(key.KeyChar.ToString()) - 1;
                else switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (--index < 0)
                                index = selectionList.Count - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            if (++index > selectionList.Count - 1)
                                index = 0;
                            break;
                        case ConsoleKey.Escape:
                            return -1;
                        case ConsoleKey.Enter:
                            return index;
                    }
                isLoop = true;

            }
        }
        public static bool IsVailUsername(string username)
        {
            return Regex.IsMatch(username, @"[a-zA-Z0-9]+");
        }
        public static bool IsVailPhoneN(string phoneN)
        {
            return Regex.IsMatch(phoneN, @"^(0[0-9]{9})+$");
        }
        public static bool IsVailName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$");
        }
        public static bool IsVailString(string text)
        {
            return Regex.IsMatch(text, @"^[(a-zA-Z0-9)][a-zA-Z0-9 ]*$");
        }
        public static void PressAnykey(string title)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
            Console.ReadKey();
            Console.ResetColor();
        }
        public static void PrintColor(string title, int key)
        {
            if (key == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(title);
                Console.ResetColor();
            }
            else if (key == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(title);
                Console.ResetColor();
            }
            
        }

        public static void DisplayTableOrderDetails(String text, List<OrderDetalis> orderd)
        {
            Console.WriteLine("\t\t\t\t╔══════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                                                                  ║");
            Console.Write("\t\t\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("                                        {0, -20}                                      ", text);
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t\t\t║                                                                                                  ║");
            Console.WriteLine("\t\t\t\t╠══════════╦═══════════════════════════╦══════════════════════╦═════════════════════════╦══════════╣");
            Console.WriteLine("\t\t\t\t║ {0, -5} ║ {1, -25} ║ {2, -20} ║ {3, -23} ║ {4 ,-8} ║ ", "Order ID", "MobilePhone Name", "unit_price", "quantity", "mID");
            for (int i = 0; i < orderd.Count; i++)
            {
                Console.WriteLine("\t\t\t\t╠══════════╬═══════════════════════════╬══════════════════════╬═════════════════════════╬══════════╣");
                Console.WriteLine("\t\t\t\t║ {0, -8} ║ {1, -25} ║ {2, -20} ║ {3, -23} ║ {4 ,-8} ║ ", orderd[i].OrderID.OrderID, orderd[i].MobilePhoneOrder.PhoneName, Money(orderd[i].total_price), orderd[i].quantity, orderd[i].MobilePhoneOrder.PhoneId);

            }
            Console.WriteLine("\t\t\t\t╚══════════╩═══════════════════════════╩══════════════════════╩═════════════════════════╩══════════╝");
        }
        public static void DisplayTableOrder(String text, List<Order> order)
        {
            Console.WriteLine("\t\t\t\t╔══════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                                                                  ║");
            Console.Write("\t\t\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("                                        {0, -20}                                      ", text);
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t\t\t║                                                                                                  ║");
            Console.WriteLine("\t\t\t\t╠══════════╦═══════════════════════════╦══════════════════════╦═════════════════════════╦══════════╣");
            Console.WriteLine("\t\t\t\t║ {0, -5} ║ {1, -25} ║ {2, -20} ║ {3, -23} ║ {4 ,-8} ║ ", "Order ID", "Customer Name", "Seller Name", "Order Date", "Status");
            for (int i = 0; i < order.Count; i++)
            {
                string status;
                if (order[i].Status == 1)
                {
                    status = "UNPAID";
                }
                else if (order[i].Status == 2)
                {
                    status = "PAID";
                }
                else
                {
                    status = "CANCEL";
                }
                Console.WriteLine("\t\t\t\t╠══════════╬═══════════════════════════╬══════════════════════╬═════════════════════════╬══════════╣");
                Console.WriteLine("\t\t\t\t║ {0, -8} ║ {1, -25} ║ {2, -20} ║ {3, -23} ║ {4 ,-8} ║ ", order[i].OrderID, order[i].OrderCustomer.CustomerName, order[i].OrderAccount.Name, order[i].Order_date, status);

            }
            Console.WriteLine("\t\t\t\t╚══════════╩═══════════════════════════╩══════════════════════╩═════════════════════════╩══════════╝");
        }
        public static void DisplayTableMobiPhone(string text, List<MobilePhone> list, int check)
        {
            Console.WriteLine("\t\t╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                                                                                                      ║");
            Console.Write("\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("                                                 {0, -30}                                                       ", text);
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t║                                                                                                                                      ║");
            Console.WriteLine("\t\t╠═══════╦════════════════════════════════╦═══════════════════════════╦════════════════════════════════╦═════════╦══════════════════════╣");
            Console.WriteLine("\t\t║ {0, -5} ║ {1, -30} ║ {2, -25} ║ {3, -30} ║ {4, -7} ║ {5, -20} ║", "ID", "Mobile Phone Name", "Operating System", "Chip", "Memory", "Price (VND)");
            if (check == 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("\t\t╠═══════╬════════════════════════════════╬═══════════════════════════╬════════════════════════════════╬═════════╬══════════════════════╣");
                    Console.WriteLine("\t\t║ {0, -5} ║ {1, -30} ║ {2, -25} ║ {3, -30} ║ {4, -7} ║ {5, -20} ║", list[i].PhoneId, list[i].PhoneName, list[i].OperatingSystem, list[i].Chip, list[i].Memory, Money(list[i].Price));
                }
            }
            else
            {
                int checki;
                if (check + 5 < list.Count)
                {
                    checki = check + 5;
                }
                else
                {
                    checki = list.Count;
                }
                for (int i = check - 1; i < checki; i++)
                {
                    Console.WriteLine("\t\t╠═══════╬════════════════════════════════╬═══════════════════════════╬════════════════════════════════╬═════════╬══════════════════════╣");
                    Console.WriteLine("\t\t║ {0, -5} ║ {1, -30} ║ {2, -25} ║ {3, -30} ║ {4, -7} ║ {5, -20} ║", list[i].PhoneId, list[i].PhoneName, list[i].OperatingSystem, list[i].Chip, list[i].Memory, Money(list[i].Price));
                }
            }
            Console.WriteLine("\t\t╚═══════╩════════════════════════════════╩═══════════════════════════╩════════════════════════════════╩═════════╩══════════════════════╝");
        }
        public static void DisplayOrder(string text)
        {
            Console.WriteLine("\t\t╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                                                                                                      ║");
            Console.Write("\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("                                                   {0, -20}                                                           ", text);
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t║                                                                                                                                      ║");
            Console.WriteLine("\t\t╠═══════╦════════════════════════════════╦═══════════════════════════╦════════════════════════════════╦═════════╦══════════════════════╣");
            Console.WriteLine("\t\t║ {0, -5} ║ {1, -30} ║ {2, -25} ║ {3, -30} ║ {4, -7} ║ {5, -20} ║", "ID", "Mobile Phone Name", "Operating System", "Chip", "Memory", "Price (VND)");
        }
        public static void DisplayPay(Order o, int check, List<OrderDetalis> od)
        {
            string text;
            long total_money = 0;
            if (check == 1)
            {
                text = "════ INVOICE ════";
            }
            else
            {
                text = "Information Order";
            }
            Console.WriteLine("\t\t\t\t╔════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                                                                        ║");
            Console.Write("\t\t\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t                               {0, -40}                          ", text);
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t\t\t╠════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
            if (check != 1)
            {
                Console.WriteLine("\t\t\t\t║ Order ID              : {0, -79}║", o.OrderID);
                Console.WriteLine("\t\t\t\t║ Customer Name         : {0 , -79}║", o.OrderCustomer.CustomerName);
                Console.WriteLine("\t\t\t\t║ Customer Phone Number : 0{0,-78}║", o.OrderCustomer.PhoneNumber);
                Console.WriteLine("\t\t\t\t║ Customer Address      : {0 , -79}║", o.OrderCustomer.CustomerAddress);
            }
            else
            {
                Console.WriteLine("\t\t\t\t║ Invoice no            : {0, -79}║", o.OrderID);
                Console.WriteLine("\t\t\t\t║ Invoice Date          : {0 , -79}║", o.Order_date);
                Console.WriteLine("\t\t\t\t╠════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine("\t\t\t\t║ Store                 : {0,-79}║", "CT Store");
                Console.WriteLine("\t\t\t\t║ Phone                 : {0 , -79}║", "0388602526");
                Console.WriteLine("\t\t\t\t║ Address               : {0 , -79}║", "VTCA Online / Tam Trinh");
                Console.WriteLine("\t\t\t\t╠════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine("\t\t\t\t║ Customer Name         : {0 , -79}║", o.OrderCustomer.CustomerName);
                Console.WriteLine("\t\t\t\t║ Customer Phone Number : 0{0,-78}║", o.OrderCustomer.PhoneNumber);
                Console.WriteLine("\t\t\t\t║ Customer Address      : {0 , -79}║", o.OrderCustomer.CustomerAddress);
            }
            Console.WriteLine("\t\t\t\t║                                                                                                        ║");
            Console.WriteLine("\t\t\t\t║┌──────┬────────────────────────────────┬──────────────────────┬──────────────────────┬────────────────┐║");
            Console.WriteLine("\t\t\t\t║│{0, -5} │ {1, -30} │ {2, -20} │ {3 ,-20} │ {4 ,-15}│║", "NO", "Phone Name", "Price", "Quantity", "Amount(VND)");
            for (int j = 0; j < od.Count; j++)
            {
                total_money = total_money + (od[j].MobilePhoneOrder.Price*od[j].quantity);
                Console.WriteLine("\t\t\t\t║├──────┼────────────────────────────────┼──────────────────────┼──────────────────────┼────────────────┤║");
                Console.WriteLine("\t\t\t\t║│{0, -5} │ {1, -30} │ {2, -20} │ {3 ,-20} │ {4 ,-15}│║", j + 1, od[j].MobilePhoneOrder.PhoneName, Money(od[j].MobilePhoneOrder.Price), od[j].quantity, Money(od[j].MobilePhoneOrder.Price*od[j].quantity));
            }
            Console.WriteLine("\t\t\t\t║├──────────────────────────────────────────────────────────────────────────────────────────────────────┤║");
            Console.WriteLine("\t\t\t\t║│{0, -5}                                                                        : {1 ,-15}│║", "total payment", Money(total_money));

            Console.WriteLine("\t\t\t\t║└──────────────────────────────────────────────────────────────────────────────────────────────────────┘║");
            if (check == 1)
            {
                Console.WriteLine("\t\t\t\t║                                                                                                        ║");
                Console.WriteLine("\t\t\t\t║                   SELLER                                        CUSTOMER                               ║");
                Console.WriteLine("\t\t\t\t║                                                                                                        ║");
                Console.WriteLine("\t\t\t\t║              {0, -40}        {1 , -40}  ║", o.OrderAccount.Name, o.OrderCustomer.CustomerName);

            }
            Console.WriteLine("\t\t\t\t╚════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
        public static void DisplayPhoneByID(string text, MobilePhone phone)
        {
            Console.WriteLine("\t\t\t\t╔═════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                             ║");
            Console.Write("\t\t\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t     {0, -36}     ", text);
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t\t\t║                                                             ║");
            Console.WriteLine("\t\t\t\t╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine("\t\t\t\t║ ID                 : {0, -38} ║", phone.PhoneId);
            Console.WriteLine("\t\t\t\t║ Mobile Phone Name  : {0, -38} ║", phone.PhoneName);
            Console.WriteLine("\t\t\t\t║ Chip               : {0, -38} ║", phone.Chip);
            Console.WriteLine("\t\t\t\t║ Memory             : {0, -38} ║", phone.Memory);
            Console.WriteLine("\t\t\t\t║ Camera             : {0, -38} ║", phone.Camera);
            Console.WriteLine("\t\t\t\t║ Operating System   : {0, -38} ║", phone.OperatingSystem);
            Console.WriteLine("\t\t\t\t║ Weight             : {0, -38} ║", phone.Weight);
            Console.WriteLine("\t\t\t\t║ Pin                : {0, -38} ║", phone.Pin);
            Console.WriteLine("\t\t\t\t║ Warranty Period    : {0, -38} ║", phone.WarrantyPeriod);
            Console.WriteLine("\t\t\t\t║ Price (VND)        : {0, -38} ║", Money(phone.Price));
            Console.WriteLine("\t\t\t\t║ Amount             : {0, -38} ║", phone.Amount);
            Console.WriteLine("\t\t\t\t╚═════════════════════════════════════════════════════════════╝");
        }
        public static String Money(long money)
        {
            string text = string.Format("{0:c0}", money);
            return text;
        }
        public static long InputMoney()
        {
            string money = "0";
            long money2 = 0;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    continue;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (money == "")
                    {
                        continue;
                    }
                    else
                    {
                        money = money.Remove(money.Length - 1);
                        Console.Write("\r                                                 \r");
                        if (money.Length >= 1)
                        {
                            money2 = long.Parse(money);
                            Console.Write(string.Format("{0:c0}", money2));
                        }
                        continue;
                    }
                }
                if (key.Key == ConsoleKey.X)
                {
                    return -1;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    return -2;
                }
                else
                {
                    if (money.Length > 9)
                    {
                        continue;
                    }
                    else if (key.KeyChar >= '0' && key.KeyChar <= '9')
                    {
                        money = money + key.KeyChar;
                        money2 = long.Parse(money);
                        Console.Write("\r                                                 \r");
                        Console.Write(string.Format("{0:c0}", money2));
                    }
                    else
                    {
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return money2;
        }
        public static void DisplayAcount(string text, List<Account> list)
        {
            string text2;
            Console.WriteLine("\t\t\t\t╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                                                                                  ║");
            Console.Write("\t\t\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("                                       {0, -20}                                                 ", text);
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t\t\t║                                                                                                                  ║");
            Console.WriteLine("\t\t\t\t╠═══════╦═══════════════════════════╦══════════════════════╦══════════════════════╦═══════════════════╦════════════╣");
            Console.WriteLine("\t\t\t\t║ {0, -5} ║ {1, -25} ║ {2, -20} ║ {3, -20} ║ {4, -17} ║ {5, -10} ║", "ID", "Name", "Phone Number", "User Name", "Password", "Status");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].AccountRole == 3)
                {
                    text2 = "Lock";
                }
                else
                {
                    text2 = "Online";
                }
                Console.WriteLine("\t\t\t\t╠═══════╬═══════════════════════════╬══════════════════════╬══════════════════════╬═══════════════════╬════════════╣");
                Console.WriteLine("\t\t\t\t║ {0, -5} ║ {1, -25} ║ {2, -20} ║ {3, -20} ║ {4, -17} ║ {5, -10} ║", list[i].AccountId, list[i].Name, list[i].PhoneN, list[i].Username, list[i].Password, text2);
            }
            Console.WriteLine("\t\t\t\t╚═══════╩═══════════════════════════╩══════════════════════╩══════════════════════╩═══════════════════╩════════════╝");
        }
    }
}