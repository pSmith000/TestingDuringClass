using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestingDuringClass
{
    class Animation
    {
        public void UpMovement()
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

        public void DownMovement()
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

        public void LeftMovement()
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

        public void RightMovement()
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

        public void Background()
        {
            Console.SetCursorPosition(0, 12);
            Console.WriteLine("          .-~~~-.\n" +
                "  .- ~ ~-(       )_ _\n" +
                " /                     ~ -.\n" +
                @"|                           \");
            Console.WriteLine(@" \                         .'");
            Console.Write("  ~- . _____________ . -~");
        }
    }
}
