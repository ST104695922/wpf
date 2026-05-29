using System;
using System.Media;

namespace CybersecurityChatbot_wpf_
{
    public class AudioHelper
    {//start of class

        private string full_path = AppDomain.CurrentDomain.BaseDirectory;

        public void PlayGreeting()
        {//start of PlayGreeting

            try
            {//start of try

                string correct_path = full_path.Replace(@"bin\Debug\", "greet.wav");

                SoundPlayer greet = new SoundPlayer(correct_path);
                greet.Play();

            }//end of try
            catch (Exception ex)
            {//start of catch
                Console.WriteLine("(Audio Error): " + ex.Message);
            }//end of catch

        }//end of PlayGreeting

    }//end of class

}//end of namespace