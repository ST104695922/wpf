using System.Collections.Generic;

namespace CybersecurityChatbot
{//
   
        internal class UserProfile
        {//start of class

            public string DisplayName { get; set; } = string.Empty;

            public string SavedTopic { get; set; } = string.Empty;

            private Dictionary<string, string> _data_store = new Dictionary<string, string>();

            public void Remember(string key, string value)
            {//start of Remember
                _data_store[key] = value;
            }//end of Remember


            public string Retrieve(string key)
            {//start of Retrieve

                if (_data_store.ContainsKey(key))
                    return _data_store[key];

                return string.Empty;

            }//end of Retrieve

            public string BuildIntro()
            {//start of BuildIntro

                if (!string.IsNullOrEmpty(SavedTopic))
                {
                    return "As someone who is interested in " + SavedTopic +
                           ", this tip is especially relevant for you, " + DisplayName + ".\n\n";
                }

                return string.Empty;

            }//end of BuildIntro


        }//end of class

    }//end of namespace
