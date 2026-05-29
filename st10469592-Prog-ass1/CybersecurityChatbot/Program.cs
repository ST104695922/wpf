using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbot
{
    public class Program
    {//start of class

        static void Main(string[] args)
        {//start of main method

            Console.Title = "Cybersecurity Awareness Bot";

            Console.BackgroundColor = ConsoleColor.Black;

            Console.Clear();

            // create instances of our helper classes


            AudioHelper audio = new AudioHelper();

            DisplayHelper display = new DisplayHelper();

            audio.PlayGreeting();

            display.ShowLogo();

            System.Threading.Thread.Sleep(800);

            ChatBot bot = new ChatBot();

            bot.GreetUser();

            bot.Run();



            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n  Thank you for using the Cybersecurity Awareness Bot!");
            Console.WriteLine("  Press any key twice to close...");
            Console.ResetColor();

            Console.ReadKey();

        }//end of main method

    }//end of class


}//end of namespace

