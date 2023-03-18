using System;
using RouteComposer.Controllers;

namespace RouteComposer
{
    class Program
    {
        public static RoutesControllerConsole routes = new RoutesControllerConsole(); 
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(routes.GetPrompt());
                var result = routes.OnNewInput(Console.ReadLine());
                if (!string.IsNullOrEmpty(result)) Console.WriteLine(result);
            }
        }
    }
}
