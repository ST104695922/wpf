using System;
using System.Media;

namespace CybersecurityChatbot
{
    public class AudioHelper
    {//start of class

        private string full_path = AppDomain.CurrentDomain.BaseDirectory;

        // method to play the WAV greeting file when the app starts
        public void PlayGreeting()
        {//start of PlayGreeting method

            try
            {//start of try

                string correct_path = full_path.Replace(@"bin\Debug\", "greet.wav");

                // create an instance of SoundPlayer
                SoundPlayer greet = new SoundPlayer(correct_path);

                // play the sound
                greet.Play();

            }//end of try
            catch (Exception ex)
            {//start of catch

                // audio errors must never crash the GUI app - silently continue
                Console.WriteLine("(Audio Error): " + ex.Message);

            }//end of catch

        }//end of PlayGreeting method


    }//end of class

}//end of namespace