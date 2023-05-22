using System;
using BL;
using Persistence;
using static System.Console;

namespace ConsolePL
{
    public class Accountant
    {
        public void DiaplayAccountant()
        {
            OrderDetalis od = new OrderDetalis();
            List<OrderDetalis> odl = new List<OrderDetalis>();
            MobilePhoneBLL mBLL = new MobilePhoneBLL();
            MobilePhone? m = new MobilePhone();
            List<MobilePhone> phones;
            OderBL oBL = new OderBL();
            Order o = new Order();
            CustomerBL cBL = new CustomerBL();
            Customer c = new Customer();
            List<Order> orders = new List<Order>();
            string[] options = { "Payment", "Exit" };
            string title = @"Mobile Phone Management";
            string title1 = @"[MENU ACCOUNTANT]";
            string line = "\t\t\t\t--------------------------------------------------------";
            string line1 = "--------------------------------------------------------";
            char isContinue;
            Keyboard keyboard = new Keyboard(title1, options);
            int SelectedIndex;
            do
            {
                SelectedIndex = keyboard.Run();
                switch (SelectedIndex)
                {
                    case 0:
                        int countOrderId = 0;
                        int count1 = 1;
                        int? CountOrder = 0;
                        ConsoleKey keyPressed3;
                        do
                        {
                            Console.Clear();
                            orders = oBL.DisplayOder(countOrderId);
                            Utility.DisplayTableOrder("Order Information", orders);
                            if (oBL.Count() % 10 != 0)
                            {
                                CountOrder = oBL.Count() / 10 + 1;
                            }
                            else
                            {
                                CountOrder = oBL.Count() / 10;
                            }
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\t\t\t\t\t\t\t    <   [{0}/{1}]   >", count1, (CountOrder));
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\t\t ● Press 'P' to payment");
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
                            else if (keyPressed3 == ConsoleKey.P)
                            {
                                Console.WriteLine(line);
                                int id = Utility.InputNumber("\t\t\t\tEnter the order id you want to payment: ", 1);
                                o = oBL.DisplayOrderById(id);
                                odl = oBL.DisplayAllOdersDetails(id , 1);
                                long total_money = 0;
                                for(int i = 0 ; i < odl.Count ; i++){
                                    total_money = total_money + (odl[i].MobilePhoneOrder.Price*odl[i].quantity);
                                }
                                if (o.Status == 1)
                                {
                                    Console.Clear();
                                    ConsoleKey keyPressed4;
                                    do
                                    {
                                        Console.Clear();
                                        Utility.DisplayPay(o, 1 , odl);
                                        Console.WriteLine("● Enter money to payment (Press 'Esc' to exit, 'X' to cancel) : ");
                                        ConsoleKeyInfo keyInfo4 = ReadKey(true);
                                        keyPressed4 = keyInfo4.Key;

                                        if (o.Status != 1)
                                        {
                                            Utility.PrintColor("\t\t\t\tCan't find order with ID : " + id,2);
                                            keyPressed4 = ConsoleKey.Escape;
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Utility.DisplayPay(o, 1 , odl);
                                            Console.WriteLine("● Enter money to payment (Press 'Esc' to exit, 'X' to cancel) : ");
                                            int money = (int)Utility.InputMoney();
                                            Console.WriteLine();
                                            if (money == -2)
                                            {
                                                break;
                                            }
                                            else if(money == -1){
                                                oBL.ChangeStatus(3 , id);
                                                Utility.PrintColor("Cancel successfully",1);
                                                Console.ReadKey();
                                                break;
                                            }
                                            else if (money < total_money)
                                            {
                                                int i = 0;
                                                while (money < total_money)
                                                {
                                                    Utility.PrintColor("The amount is not enough to make the payment",2);
                                                    Console.WriteLine("● Enter money to payment (Press 'Esc' to exit, 'X' to cancel) : ");
                                                    money = (int)Utility.InputMoney();
                                                    Console.WriteLine();
                                                    if (money == -1)
                                                    {
                                                        o = oBL.DisplayOrderById(id);
                                                        if (o.Status != 1)
                                                        {
                                                            Utility.PrintColor("\t\t\t\tCancel order not completed",2);
                                                            keyPressed4 = ConsoleKey.Escape;

                                                        }
                                                        else
                                                        {
                                                            oBL.ChangeStatus(0, id);
                                                            Utility.PrintColor("\t\t\t\tCancel order completed",1);
                                                            keyPressed4 = ConsoleKey.Escape;

                                                        }
                                                        Utility.PressAnykey("\t\t\t\tPress any key to exit...");
                                                        break;
                                                    }
                                                    else if (money == -2)
                                                    {
                                                        i = -1;
                                                        break;
                                                    }
                                                    else if(money == -1){
                                                        i = -1;
                                                        oBL.ChangeStatus(3 , id);
                                                        Console.WriteLine("Cancel successfully");
                                                        Console.ReadKey();
                                                    }
                                                }
                                                if (i != -1)
                                                {
                                                    Console.WriteLine(line1);
                                                    Console.WriteLine("Escess cash : " + Utility.Money(money - total_money));
                                                    oBL.ChangeStatus(2, id);
                                                    keyPressed4 = ConsoleKey.Escape;
                                                    Utility.PressAnykey("Press any key to exit...");
                                                    break;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            Console.WriteLine(line1);
                                            
                                            Console.WriteLine("Escess cash : " + Utility.Money(money - total_money));
                                            oBL.ChangeStatus(2, id);

                                            keyPressed4 = ConsoleKey.Escape;
                                            Utility.PressAnykey("Press any key to exit...");
                                        }
                                    } while (keyPressed4 != ConsoleKey.Escape);
                                }
                                else if (o.Status == 2)
                                {
                                    Utility.PrintColor("\t\t\t\tThe order has been PAID",2);
                                    Utility.PressAnykey("\t\t\t\tPress any key to go back...");
                                }
                                else if (o.Status == 3)
                                {
                                    Utility.PrintColor("\t\t\t\tThe order has been CANCELED",2);
                                    Utility.PressAnykey("\t\t\t\tPress any key to go back...");
                                }
                                else
                                {
                                    Utility.PrintColor("\t\t\t\tCan't find mobile phone with ID : " + id,2);
                                    keyPressed3 = ConsoleKey.Escape;
                                    Utility.PressAnykey("\t\t\t\tPress any key to go back...");
                                }
                                keyPressed3 = ConsoleKey.Enter;
                            }
                        } while (keyPressed3 != ConsoleKey.Escape);
                    break;
                }
            } while (SelectedIndex != 1);
            Environment.Exit(0);
        }
    }
}