using System;
using BL;
using Persistence;
using static System.Console;

namespace ConsolePL
{
    public class Keyboard
    {
        private int SelectedIndex;
        private string[] Options;
        private string Title;

        public Keyboard(string title, string[] options)
        {
            Title = title;
            Options = options;
            SelectedIndex = 0;
        }
        public Keyboard(){}


        private void DisplayOptions()
        {
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
            Console.Write("\t\t\t\t║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"                               {Title,-48}");
            Console.ResetColor();
            Console.WriteLine("║");
            Console.WriteLine("\t\t\t\t║                                                                               ║");
            Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("\t\t\t\t║                                                                               ║");
            // Console.ForegroundColor = ConsoleColor.Magenta;
            // Console.ResetColor();

            for (int i = 0; i < Options.Length; i++)
            {

                string currentIndex = Options[i];
                string prefex;
                if (i == SelectedIndex)
                {
                    prefex = ">";
                    // ForegroundColor = ConsoleColor.Black;
                    // BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefex = " ";
                    // ForegroundColor = ConsoleColor.White;
                    // BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"\t\t\t\t║                          {prefex}    {currentIndex,-44}    ║");
            }
            Console.WriteLine("\t\t\t\t║                                                                               ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════════════════════════════╝");
            ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}