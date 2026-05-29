using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    internal class BotEngine
    {//start of namespace

      

            private TopicHandler _topics;
            private MoodReader _mood;
            private UserProfile _profile;

            private bool _needsName = true;

            private string _recentTopic = string.Empty;

            private List<string> _continueWords = new List<string>
        {
            "tell me more", "more", "explain more", "give me more",
            "another tip", "go on", "what else", "continue",
            "elaborate", "keep going", "more info"
        };


            public BotEngine()
            {//start of constructor
                _topics = new TopicHandler();
                _mood = new MoodReader();
                _profile = new UserProfile();
            }//end of constructor


            // OpeningMessage - shown when the app first loads
            public string OpeningMessage()
            {//start of OpeningMessage
                return "Hello! Welcome to the Cybersecurity Awareness Bot.\n" +
                       "Before we start, please tell me your name.";
            }//end of OpeningMessage


         
            public string FetchName()
            {//start of FetchName
                return _profile.DisplayName;
            }//end of FetchName

             public string HandleMessage(string raw_input)
            {//start of HandleMessage

                if (string.IsNullOrWhiteSpace(raw_input))
                    return "Please type something so I can help you.";

                string lowered = raw_input.ToLower().Trim();

                if (_needsName)
                {
                    _profile.DisplayName = raw_input.Trim();
                    _needsName = false;

                    return "Nice to meet you, " + _profile.DisplayName + "!\n\n" +
                           "I am your Cybersecurity Awareness Bot.\n" +
                           "Here is what you can ask me about:\n\n" +
                           "  - Passwords\n" +
                           "  - Phishing and scams\n" +
                           "  - Privacy\n" +
                           "  - Safe browsing\n" +
                           "  - Malware and viruses\n" +
                           "  - Virtual Private Networks\n" +
                           "  - Firewalls\n" +
                           "  - Two-factor authentication\n" +
                           "  - Fraud\n\n" +
                           "You can also tell me how you are feeling and I will respond accordingly.\n" +
                           "Type 'help' at any time to see this list again.";
                }
                       foreach (string phrase in _continueWords)
                {
                    if (lowered.Contains(phrase) && !string.IsNullOrEmpty(_recentTopic))
                    {
                        string extra = _topics.FindAnswer(_recentTopic);
                        return _profile.BuildIntro() + extra;
                    }
                }
                   if (lowered.Contains("interested in") || lowered.Contains("i like") || lowered.Contains("i care about"))
                {
                    string extracted = PullTopicFromSentence(lowered);
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        _profile.SavedTopic = extracted;
                        _profile.Remember("interest", extracted);

                        string related = _topics.FindAnswer(extracted);
                        if (!string.IsNullOrEmpty(related))
                        {
                            _recentTopic = extracted;
                            return "I will remember that you are interested in " + extracted + ".\n\n" + related;
                        }
                        return "I will remember that you are interested in " + extracted + ". Feel free to ask me about it anytime.";
                    }
                }

                UserMood detected_mood = _mood.CheckMood(lowered);
                string mood_opener = _mood.MoodReply(detected_mood);

                string topic_answer = _topics.FindAnswer(lowered);

                if (!string.IsNullOrEmpty(topic_answer))
                {
                    _recentTopic = _topics.FindKeyword(lowered);

                    if (!string.IsNullOrEmpty(mood_opener))
                        return mood_opener + "\n\n" + topic_answer;

                    return _profile.BuildIntro() + topic_answer;
                }
                if (!string.IsNullOrEmpty(mood_opener))
                {
                    _recentTopic = "general";
                    return mood_opener + "\n\n" + _topics.RandomTip();
                }

                if (lowered.Contains("how are you") || lowered.Contains("how r u"))
                    return "I am running perfectly and ready to help you, " + _profile.DisplayName + "!";

                if (lowered.Contains("help") || lowered.Contains("what can you do") || lowered.Contains("topics"))
                    return BuildTopicList();

                if (lowered.Contains("purpose") || lowered.Contains("what are you"))
                    return "My purpose is to educate you on cybersecurity and help you stay safe online, " +
                           _profile.DisplayName + ". Ask me about passwords, phishing, malware, privacy, or any cybersecurity topic.";

                if (lowered.Contains("hello") || lowered.Contains("hi") || lowered.Contains("hey"))
                    return "Hello, " + _profile.DisplayName + "! How can I help you with cybersecurity today?";

                // STEP 7: Fallback
                return PickFallback();

            }//end of HandleMessage


           private string PullTopicFromSentence(string input)
            {//start of PullTopicFromSentence

                string[] markers = { "interested in", "i like", "i care about" };

                foreach (string marker in markers)
                {
                    int pos = input.IndexOf(marker);
                    if (pos >= 0)
                    {
                        string after = input.Substring(pos + marker.Length).Trim().TrimEnd('.', '!', '?');
                        if (!string.IsNullOrEmpty(after))
                            return after;
                    }
                }

                return string.Empty;

            }//end of PullTopicFromSentence


            private string BuildTopicList()
            {//start of BuildTopicList

                string listing = "Here is what you can ask me about, " + _profile.DisplayName + ":\n\n" +
                                 "  - Passwords\n" +
                                 "  - Phishing and scams\n" +
                                 "  - Privacy\n" +
                                 "  - Safe browsing\n" +
                                 "  - Malware and viruses\n" +
                                 "  - Virtual Private Networks\n" +
                                 "  - Firewalls\n" +
                                 "  - Two-factor authentication\n" +
                                 "  - Fraud\n\n" +
                                 "After any response, type 'tell me more' to get another tip on the same topic.";

                if (!string.IsNullOrEmpty(_profile.SavedTopic))
                    listing += "\n\nI remember you are interested in " + _profile.SavedTopic + ". Would you like a tip on that?";

                return listing;

            }//end of BuildTopicList


            private string PickFallback()
            {//start of PickFallback

                string[] options =
                {
                "I am not sure I understand that, " + _profile.DisplayName + ". Could you rephrase? Try asking about passwords, phishing, malware, or privacy.",
                "That is outside what I know about, " + _profile.DisplayName + ". Ask me about cybersecurity topics like safe browsing, VPNs, or firewalls.",
                "I did not quite catch that. Type 'help' to see all the topics I can assist with.",
                "I am not able to answer that just yet. Ask me about online safety and I will do my best to help, " + _profile.DisplayName + "."
            };

                Random rng = new Random();
                return options[rng.Next(options.Length)];

            }//end of PickFallback


        }//end of class

    }//end of namespace