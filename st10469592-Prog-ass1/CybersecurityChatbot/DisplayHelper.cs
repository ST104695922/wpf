using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace CybersecurityChatbot
{
    //start of namespace
    //most comments are for presentation purposes
    public class DisplayHelper
    {//start of class

        // auto get the path directory
        private string full_path = AppDomain.CurrentDomain.BaseDirectory;

        // property to hold the users name - updated from ChatBot after name is entered
        public string UserName { get; set; } = "YOU";

       

        // method to show the image as ASCII art when the app starts
        public void ShowLogo()

        {//start of ShowLogo method

            try
            {//start of try - attempt to load and convert the image

                Console.WriteLine(full_path);

                string correct_path = full_path.Replace(@"bin\Debug\", "logo.png");

                Console.WriteLine(correct_path);

                if (!File.Exists(correct_path))
                {//start of if file not found
                    throw new FileNotFoundException("logo.png was not found at: " + correct_path);
                }//end of if

                Bitmap originalImage = new Bitmap(correct_path);

                int width = 80;
                int height = 35;
                Bitmap resizedImage = new Bitmap(originalImage, new Size(width, height));

                string asciiChars = "@#S%?*+;:,. ";

                Console.ForegroundColor = ConsoleColor.Magenta;

                for (int y = 0; y < resizedImage.Height; y++)
                {//start of height loop

                    for (int x = 0; x < resizedImage.Width; x++)
                    {//start of width loop

                        Color pixel = resizedImage.GetPixel(x, y);

                        int gray = (pixel.R + pixel.G + pixel.B) / 3;

                        int index = (gray * (asciiChars.Length - 1)) / 255;

                        // print that one ASCII character
                        Console.Write(asciiChars[index]);

                    }//end of width loop

                    // after each row of pixels move to the next line
                    Console.WriteLine();

                }//end of height loop

                Console.ResetColor();

                // print the bot title under the image
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n        C Y B E R S E C U R I T Y   A W A R E N E S S   B O T  ");
                Console.ResetColor();

                ShowDivider();

            }//end of try
            catch (Exception ex)
            {//start of catch - runs if anything above failed

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" Image Error: " + ex.Message);
                Console.ResetColor();

                ShowTextFallback();

            }//end of catch

        }//end of ShowLogo method


        // fallback method if image fails to load
        private void ShowTextFallback()
        {//start of ShowTextFallback method

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
        +-------------------------------------------------+
        |                                                 |
        |              CYBERSECURITY BOT                  |
        |                                                 |
        |                  _______                        |
        |                 |       |                       |
        |                 |  ( )  |                       |
        |                 |_______|                       |
        |                 |       |                       |
        |                 |_______|                       |
        |                                                 |
        +-------------------------------------------------+
            ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  C Y B E R S E C U R I T Y   A W A R E N E S S   B O T  ");
            Console.ResetColor();

            ShowDivider();

        }//end of ShowTextFallback 


        // method to print a divider line 
        public void ShowDivider()
        {//start of ShowDivider method

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(new string('═', 60));
            Console.ResetColor();

        }//end of ShowDivider method


        // method to print what the bot says
        public void BotSay(string message)
        {//start of BotSay method

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("\n ChatBot : ");   
            Console.WriteLine(message);
            Console.ResetColor();

        }//end of BotSay method


        // method to show the users name prompt so they know its their turn
        public void UserPrompt()
        {//start of UserPrompt method

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n " + UserName + " : ");  // shows their name instead of YOU
            Console.ResetColor();

        }//end of UserPrompt method


        // method to print text one character at a time
        public void TypeEffect(string text, ConsoleColor color)
        {//start of TypeEffect method

            Console.ForegroundColor = color;

            // loop through every character in the text
            foreach (char letter in text)
            {
                Console.Write(letter);
                Thread.Sleep(25);
            }

            Console.WriteLine();
            Console.ResetColor();

        }//end of TypeEffect method


    }//end of class

}//end of namespace