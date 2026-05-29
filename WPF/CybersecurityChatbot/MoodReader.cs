using System.Collections.Generic;

namespace CybersecurityChatbot
{
   
        public enum UserMood
        {
            None,
            Anxious,
            Inquisitive,
            Upset,
            Positive
        }

        public class MoodReader
        {//start of class

            private Dictionary<UserMood, List<string>> _mood_words;


            public MoodReader()
            {//start of constructor
                _mood_words = new Dictionary<UserMood, List<string>>();
                SetupMoodWords();
            }//end of constructor

            public UserMood CheckMood(string input)
            {//start of CheckMood

                string lowered = input.ToLower();

                foreach (UserMood mood in _mood_words.Keys)
                {
                    foreach (string trigger in _mood_words[mood])
                    {
                        if (lowered.Contains(trigger))
                            return mood;
                    }
                }

                return UserMood.None;

            }//end of CheckMood


            public string MoodReply(UserMood mood)
            {//start of MoodReply

                switch (mood)
                {
                    case UserMood.Anxious:
                        return "It is completely understandable to feel that way about cybersecurity threats. " +
                               "The good news is that most risks can be managed effectively with the right knowledge and habits. " +
                               "Let me share something that should help put your mind at ease.";

                    case UserMood.Inquisitive:
                        return "That is exactly the right attitude to have when it comes to staying safe online. " +
                               "Asking questions is one of the most effective ways to build your cybersecurity awareness. " +
                               "Here is what you should know.";

                    case UserMood.Upset:
                        return "I completely understand your frustration. " +
                               "Cybersecurity topics can feel overwhelming, but let us work through it together one step at a time. " +
                               "I will explain this as clearly as possible.";

                    case UserMood.Positive:
                        return "It is great to hear that you are feeling positive. " +
                               "That kind of energy makes learning about cybersecurity much easier. " +
                               "Here is something useful to keep in mind.";

                    default:
                        return string.Empty;
                }

            }//end of MoodReply

            private void SetupMoodWords()
            {//start of SetupMoodWords

                _mood_words[UserMood.Anxious] = new List<string>
            {
                "worried", "scared", "afraid", "anxious", "nervous",
                "unsafe", "frightened", "concerned", "overwhelmed",
                "stressed", "panicking", "terrified"
            };

                _mood_words[UserMood.Inquisitive] = new List<string>
            {
                "curious", "wondering", "interested", "want to know",
                "how does", "what is", "tell me about", "explain",
                "how do i", "how can i", "what should"
            };

                _mood_words[UserMood.Upset] = new List<string>
            {
                "frustrated", "annoyed", "confused", "dont understand",
                "don't understand", "difficult", "hard to", "struggling",
                "angry", "irritated", "lost", "no idea"
            };

                _mood_words[UserMood.Positive] = new List<string>
            {
                "great", "thanks", "helpful", "awesome", "love it",
                "happy", "good", "excellent", "fantastic", "wonderful",
                "thank you", "appreciate", "brilliant", "amazing"
            };

            }//end of SetupMoodWords


        }//end of class

    }//end of namespace