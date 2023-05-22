using Persistence;
using BL;

namespace ConsolePL
{
    class Program
    {
        public static void Main(string[] args)
        {
            Account account = new Account();
            AccountBLL accountBLL = new AccountBLL();
            SellerMenu seller = new SellerMenu();
            Accountant accountant = new Accountant();
            int count = 0;
            string title = "[LOGIN]";
            do{
                Console.Clear();
                Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t\t\t║                                                                               ║");
                Console.WriteLine(@"                                ║                                                                               ║
                                ║             _____ _______      _____ _______ ____  _____  ______              ║
                                ║            / ____|__   __|    / ____|__   __/ __ \|  __ \|  ____|             ║
                                ║            | |      | |      | (___    | | | |  | | |__) | |__                ║
                                ║            | |      | |       \___ \   | | | |  | |  _  /|  __|               ║
                                ║            | |____  | |       ____) |  | | | |__| | | \ \| |____              ║
                                ║            \_____|  |_|      |_____/   |_|  \____/|_|  \_\______|             ║");
                Console.WriteLine("\t\t\t\t║                                                                               ║");
                Console.WriteLine("\t\t\t\t║                                                                               ║");
                Console.WriteLine($"\t\t\t\t║                                   {title,-44}║");
                Console.WriteLine("\t\t\t\t║                                                                               ║");
                Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════════════════════════════╝");
                            Console.WriteLine("\n");
                            Console.Write("\t\t\t\t » Username : ");
                            string userN = Console.ReadLine() ?? "";
                            Console.Write("\t\t\t\t » Password : ");
                            string passW = "";
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
                                    if (passW == "")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        passW = passW.Remove(passW.Length - 1);
                                        Console.Write("\b \b");
                                        continue;
                                    }
                                }
                                else if(!Char.IsControl(key.KeyChar))
                                {
                                    passW = passW + key.KeyChar;
                                    Console.Write("*");
                                }
                            } while (key.Key != ConsoleKey.Enter);
                            
                            account = accountBLL.Login(userN , passW);

                            if (account.AccountRole == 1)
                            {
                                seller.DisplaySellerMenu(account);
                            }
                            else if (account.AccountRole == 2)
                            {
                                accountant.DiaplayAccountant();
                            }
                            
                            else
                            {
                                Console.WriteLine();
                                Utility.PrintColor("\t\t\t\t Incorrect username or password, please re-enter!",2);
                                Console.WriteLine();
                                Console.WriteLine("\t\t\t\t ● Press 'Esc' to exit - 'Enter' to continue");
                                ConsoleKeyInfo key1;
                                key1 = Console.ReadKey(true);
                                if (key1.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }
                                count ++;
                            }
                            Console.ResetColor();
                        if(count == 3){
                            Console.WriteLine();
                            Utility.PrintColor("\t\t\t\t Your login attempts have exceeded the limit 3 times",2);
                            Console.ReadKey();
                        }
                }while(count != 3);     
        }
    }


}