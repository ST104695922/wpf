using System.Collections.Generic;

namespace CybersecurityChatbot_wpf_
{
    //start of namespace

    /// <summary>
    /// UserProfile - stores information about the user across the conversation.
    /// Remembers the user's name and their favourite cybersecurity topic.
    /// Also provides a general key-value store for any other details.
    /// </summary>
    public class UserProfile
    {//start of class

        // the user's name, captured when they first type in the chat
        public string DisplayName { get; set; } = string.Empty;

        // the topic the user expressed interest in
        public string SavedTopic { get; set; } = string.Empty;

        // general purpose key-value store for anything else worth remembering
        private Dictionary<string, string> _data_store = new Dictionary<string, string>();


        // Remember - saves a key-value pair into the data store
        public void Remember(string key, string value)
        {//start of Remember
            _data_store[key] = value;
        }//end of Remember


        // Retrieve - gets a previously stored value by its key
        public string Retrieve(string key)
        {//start of Retrieve

            if (_data_store.ContainsKey(key))
                return _data_store[key];

            return string.Empty;

        }//end of Retrieve


        // BuildIntro - creates a personalised opening line using stored information.
        // Prepended to keyword responses when a saved topic is available.
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