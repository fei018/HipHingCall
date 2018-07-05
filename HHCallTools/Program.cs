using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHCSPHelp;

namespace HHCallTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press key \"A\" to add call or other key to exit");
            
            //Console.WriteLine("Press key other to exit.");

            ConsoleKey key = Console.ReadKey(true).Key;
            Console.WriteLine();

            if (key.Equals(ConsoleKey.A))
            {

                OnPost();

                Console.WriteLine();
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }
        }

        private static void OnPost()
        {
            try
            {
                CSPLoginSet.AppExit = true;               

                FormCall form = new FormCall();
                form.ShowDialog();

                if (CSPLoginSet.AppExit)
                {
                    Environment.Exit(0);
                }
                
                CSP2Run test = new CSP2Run();
                test.OnStart();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CSPLogger.Error2Txt(ex.Message);
            }
        }

        private static void Test()
        {
            try
            {
                CSPTest test = new CSPTest();
                test.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CSPLogger.Error2Txt(ex.Message);
            }
        }
    }
}
