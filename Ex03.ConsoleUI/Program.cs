using System;

namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            GarageManager garageManager = new GarageManager();
            garageManager.RunProgram();

            Console.WriteLine("Please press 'Enter' to exit...");
            Console.ReadLine();
        }
        
    }
}
