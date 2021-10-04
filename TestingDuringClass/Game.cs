using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace TestingDuringClass
{
    class Game
    {
        bool gameOver = false;


        public void Run()
        {
            if (!gameOver)
                Update();
        }
        void Update()
        {
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("---------");
            Console.WriteLine("|       |");

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("             ");
            Console.WriteLine("    0 ");
            Console.WriteLine(@"   /|\ ");
            Console.WriteLine("    | ");
            Console.WriteLine(@"   | | ");

            ConsoleKeyInfo cki;

            Console.TreatControlCAsInput = true;

            cki = Console.ReadKey();

            while (cki.Key != ConsoleKey.Escape)
            {

                if ((cki.Key.ToString() == "UpArrow"))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("    0 ");
                    Console.WriteLine(@"   \|/ ");
                    Console.WriteLine("    | ");
                    Console.WriteLine(@"   / \ ");
                    Console.WriteLine("         ");
                    Thread.Sleep(200);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("             ");
                    Console.WriteLine("    0 ");
                    Console.WriteLine(@"   /|\ ");
                    Console.WriteLine("    | ");
                    Console.WriteLine(@"   | | ");
                    Thread.Sleep(200);
                }
                else if ((cki.Key.ToString() == "DownArrow"))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("             ");
                    Console.WriteLine("             ");
                    Console.WriteLine(@"     0 ");
                    Console.WriteLine(@"    |\ ");
                    Console.WriteLine(@"    _| ");
                    Thread.Sleep(200);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("             ");
                    Console.WriteLine("    0 ");
                    Console.WriteLine(@"   /|\ ");
                    Console.WriteLine("    | ");
                    Console.WriteLine(@"   | | ");
                    Thread.Sleep(200);
                }
                else if ((cki.Key.ToString() == "LeftArrow"))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("             ");
                    Console.WriteLine("  0        ");
                    Console.WriteLine(@"//|       ");
                    Console.WriteLine("  |        ");
                    Console.WriteLine(@" | |      ");
                    Thread.Sleep(200);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("             ");
                    Console.WriteLine("    0 ");
                    Console.WriteLine(@"   /|\ ");
                    Console.WriteLine("    | ");
                    Console.WriteLine(@"   | | ");
                    Thread.Sleep(200);
                }
                else if ((cki.Key.ToString() == "RightArrow"))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("             ");
                    Console.WriteLine("      0 ");
                    Console.WriteLine(@"      |\\ ");
                    Console.WriteLine("      | ");
                    Console.WriteLine(@"     | | ");
                    Thread.Sleep(200);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("             ");
                    Console.WriteLine("    0        ");
                    Console.WriteLine(@"   /|\      ");
                    Console.WriteLine("    |        ");
                    Console.WriteLine(@"   | |      ");
                    Thread.Sleep(200);
                }

                cki = Console.ReadKey();
            }
        }
    }
}

