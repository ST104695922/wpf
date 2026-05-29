using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace CybersecurityChatbot
{
    public class ChatBot
    {//start of class

        public string UserName { get; set; }
        public bool IsRunning { get; set; }

        // creating an instance of DisplayHelper 
        DisplayHelper display = new DisplayHelper();


        // constructor 
        public ChatBot()
        {//start of constructor

            UserName = "Friend";
            IsRunning = true;

        }//end of constructor


        // method to greet the user and ask for their name
        public void GreetUser()
        {//start of GreetUser method

            display.BotSay("Hello there! Before we begin, what is your name?");
            display.UserPrompt();

            // read whatever the user types
            string input = Console.ReadLine();

            // input validation
            if (string.IsNullOrWhiteSpace(input))
            {//start of if 

                // they didnt type a name so keep the default
                UserName = "Friend";
                display.UserName = UserName; // sync the name to display
                display.BotSay("No name entered - I will call you Friend!");

            }//end of if
            else
            {//start of else 

                UserName = input.Trim();
                display.UserName = UserName; // sync the name to display
                display.BotSay("Nice to meet you, " + UserName + "! ");

            }//end of else

            System.Threading.Thread.Sleep(500);

            // show the user what topics they can ask about
            display.ShowDivider();
            display.BotSay("I am your Cybersecurity Awareness Bot, " + UserName + ".");
            display.BotSay("Here is what you can ask me about:");
            display.BotSay("  - What is your purpose");
            display.BotSay("  - How are you");
            Console.WriteLine();
            display.BotSay("   However you can also ask me about :");
            Console.WriteLine();
            display.BotSay("   - Password safety");
            display.BotSay("   - Phishing");
            display.BotSay("   - Safe browsing");
            Console.WriteLine();
            display.BotSay("Type 'exit' to quit at any time.");
            display.ShowDivider();

        }//end of GreetUser method


        // method to get the bots response based on what the user typed
        // takes the users message and returns the bots reply
        public string GetResponse(string userInput)
        {//start of GetResponse method

            // input check if they typed nothing
            if (string.IsNullOrWhiteSpace(userInput))
            {//start of if - empty input
                return "I did not catch that. Please type something! Type 'exit' to quit at any time.";
            }//end of if

            string input = userInput.ToLower().Trim();

            // check for exit words
            if (input == "exit" || input == "quit" || input == "bye")
            {//start of if - user wants to leave
                IsRunning = false; // this stops the loop in the Run method
                return "Goodbye, " + UserName + "! Stay safe online! ";
            }//end of if

            // check for how are you
            if (input.Contains("how are you") || input.Contains("how r u"))
            {//start of if
                return "I am just a bot but I am running great and ready to help you, " + UserName + "! ";
            }//end of if

            // check for purpose question
            if (input.Contains("purpose") || input.Contains("what do you do") || input.Contains("what are you"))
            {//start of if
                return "My purpose is to help you stay safe online! I teach you about passwords, phishing, and safe browsing.";
            }//end of if

            // check for help or topics question
            if (input.Contains("help") || input.Contains("what can i ask") || input.Contains("topics"))
            {//start of if
                return "You can ask me about:  passwords |  phishing |  safe browsing |  how are you |  your purpose";
            }//end of if

            // check for password questions
            if (input.Contains("password"))
            {//start of if
                return " Password Tips:\n" +
                       "   • Use at least 12 characters\n" +
                       "   • Mix uppercase, lowercase, numbers and symbols\n" +
                       "   • Never reuse the same password on different sites\n" +
                       "   • Use a free password manager like Bitwarden";
            }//end of if

            // check for phishing questions
            if (input.Contains("phishing") || input.Contains("scam") || input.Contains("fake email"))
            {//start of if
                return " Phishing Awareness:\n" +
                       "   • Phishing is when attackers trick you into giving personal info\n" +
                       "   • Always check the sender email address carefully\n" +
                       "   • Never click suspicious links in emails\n" +
                       "   • Legit companies will never ask for your password via email";
            }//end of if

            // check for safe browsing questions
            if (input.Contains("browsing") || input.Contains("browser") || input.Contains("safe") || input.Contains("internet"))
            {//start of if
                return " Safe Browsing Tips:\n" +
                       "   • Always look for HTTPS and the padlock icon\n" +
                       "   • Avoid downloading files from untrusted websites\n" +
                       "   • Use a VPN when on public Wi-Fi\n" +
                       "   • Keep your browser updated";
            }//end of if

            // check for greetings
            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
            {//start of if
                return "Hey " + UserName + "!  How can I help you with cybersecurity today?";
            }//end of if

            // default response 
            return "Hmm I am not sure about that, " + UserName + ". Try asking about passwords, phishing, or safe browsing!";

        }//end of GetResponse method


        // method to run the main conversation loop
        // keeps asking for input until the user types exit
        public void Run()
        {//start of Run method

            // keep looping as long as IsRunning is true
            while (IsRunning)
            {//start of while loop

                display.UserPrompt();

                string userInput = Console.ReadLine();

                string response = GetResponse(userInput);

                display.BotSay(response);

                System.Threading.Thread.Sleep(300);

            }//end of while loop

        }//end of Run method


    }//end of class

}//end of namespace
