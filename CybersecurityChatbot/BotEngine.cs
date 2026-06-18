using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CybersecurityChatbot
{
    internal class BotEngine
    {//start of class

        
        private TopicHandler _topics;
        private MoodReader _mood;
        private UserProfile _profile;
        //neww tasks
        private DatabaseHelper _db;
        private QuizEngine _quiz;
        private ActivityLog _log;

        private bool needsName = true;
        private string recentTopic = string.Empty;

        // task flow
        private bool waitingForTitle = false;
        private bool waitingForDesc = false;
        private bool waitingForReminder = false;
        private bool waitingForDays = false;
        private string pendingTitle = string.Empty;
        private string pendingDesc = string.Empty;

        private List<string> continueWords = new List<string>
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
            _db = new DatabaseHelper();
            _quiz = new QuizEngine();
            _log = new ActivityLog();

        }//end of constructor


        public string OpeningMessage()
        {//start of OpeningMessage

            return "Hello! Welcome to the Cybersecurity Awareness Bot.\n" +
                   "Before we start, please tell me your nameee !";

        }//end of OpeningMessage


        public string FetchName()
        {//start of FetchName

            return _profile.DisplayName;
        }//end of FetchName


        public string HandleMessage(string input)
        {//start of HandleMessage

            if (string.IsNullOrWhiteSpace(input))
                return "Please type something so I can help you.";

            string low = input.ToLower().Trim();

            // get the users name on the first message
            if (needsName)
            {
                _profile.DisplayName = input.Trim();
                needsName = false;
                _log.Add("Session started for " + _profile.DisplayName);

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
                       "  Additionally (tasks):\n" +
                       "  - Type 'add task' to save a cybersecurity task\n" +
                       "  - Type 'view tasks' to see your tasks\n" +
                       "  - Type 'start quiz' to test your knowledge\n" +
                       "  - Type 'show log' to see recent activity\n\n" +
                       "You can also tell me how you are feeling and I will respond accordingly.";
            }

            // if the quiz is running
            if (_quiz.IsActive)
            {
                string result = _quiz.CheckAnswer(input);

                if (!_quiz.IsActive)
                    _log.Add("Quiz completed by " + _profile.DisplayName);

                return result;
            }

            // task add flow
            if (waitingForTitle)
            {
                pendingTitle = input.Trim();
                waitingForTitle = false;
                waitingForDesc = true;

                return "Got it. Give me a short description, or type 'skip'.";
            }

            // task add flow
            if (waitingForDesc)
            {
                if (low == "skip")
                    pendingDesc = "";
                else
                    pendingDesc = input.Trim();

                waitingForDesc = false;
                waitingForReminder = true;

                return "Would you like a reminder for this task? Type yes or no.";
            }

            // task add flow - step 3: waiting for yes or no on reminder
            if (waitingForReminder)
            {
                waitingForReminder = false;

                if (low == "yes" || low == "y")
                {
                    waitingForDays = true;
                    return "In how many days would you like to be reminded?";
                }

                // save without reminder
                _db.AddTask(pendingTitle, pendingDesc, "No reminder", "pending");
                _log.Add("Task added: " + pendingTitle + " (no reminder)");

                return "Task added: '" + pendingTitle + "' with no reminder.";
            }

            // task add flow - step 4: waiting for the number of days
            if (waitingForDays)
            {
                waitingForDays = false;

                // strip everything except numbers
                string numOnly = Regex.Replace(input, @"[^0-9]", "");

                if (numOnly != "" && int.Parse(numOnly) > 0)
                {
                    int days = int.Parse(numOnly);
                    string dueDate = DateTime.Now.AddDays(days).ToString("MMMM dd yyyy");

                    _db.AddTask(pendingTitle, pendingDesc, dueDate, "pending");
                    _log.Add("Task added: " + pendingTitle + " | Reminder: " + dueDate);

                    return "Task added: '" + pendingTitle + "'. I will remind you on " + dueDate + ".";
                }

                // if they typed something that isn't a number, save without reminder
                _db.AddTask(pendingTitle, pendingDesc, "No reminder", "pending");
                _log.Add("Task added: " + pendingTitle + " (no reminder, invalid input)");

                return "I did not recognise that. Task saved without a reminder.";
            }

            // activity log commands
            if (low.Contains("show log") || low.Contains("activity log") ||
                low.Contains("what have you done") || low.Contains("recent actions"))
            {
                _log.Add("Activity log viewed");
                return _log.GetLog();
            }

            if (low.Contains("show full log") || low.Contains("full log"))
            {
                return _log.GetFullLog();
            }

            // quiz commands
            if (low.Contains("start quiz") || low.Contains("quiz me") ||
                low.Contains("play quiz") || low.Contains("take quiz") || low == "quiz")
            {
                _log.Add("Quiz started by " + _profile.DisplayName);
                return _quiz.Start();
            }

            // view tasks
            if (low.Contains("view task") || low.Contains("show task") ||
                low.Contains("my tasks") || low.Contains("list task"))
            {
                _log.Add("Tasks viewed by " + _profile.DisplayName);
                return ShowTasks();
            }

            // complete a task - e.g. "complete task 2"
            if (low.Contains("complete task") || low.Contains("mark task") || low.Contains("done task"))
            {
                string numOnly = Regex.Replace(low, @"[^0-9]", "");

                if (numOnly != "")
                {
                    int id = int.Parse(numOnly);
                    _db.MarkComplete(id);
                    _log.Add("Task #" + id + " marked as completed");
                    return "Task #" + id + " has been marked as completed.";
                }

                return "Please include the task number. For example: complete task 2";
            }

            // delete a task - e.g. "delete task 3"
            if (low.Contains("delete task") || low.Contains("remove task"))
            {
                string numOnly = Regex.Replace(low, @"[^0-9]", "");

                if (numOnly != "")
                {
                    int id = int.Parse(numOnly);
                    _db.DeleteTask(id);
                    _log.Add("Task #" + id + " deleted");
                    return "Task #" + id + " has been deleted.";
                }

                return "Please include the task number. For example: delete task 3";
            }

            // add task - NLP: catches different ways the user might say it
            if (low.Contains("add task") || low.Contains("add a task") ||
                low.Contains("create task") || low.Contains("new task") ||
                low.Contains("save task") || low.Contains("i need to do"))
            {
                // try to pull a title from the sentence
                string title = GetTaskTitle(low);

                if (title != "")
                {
                    pendingTitle = title;
                    waitingForDesc = true;
                    _log.Add("Add task detected: " + title);
                    return "I will add a task called '" + title + "'. Give me a short description, or type 'skip'.";
                }

                // could not pull a title, ask for it
                waitingForTitle = true;
                _log.Add("Add task intent detected, waiting for title");
                return "Sure! What would you like to call this task?";
            }

            // remind me to - NLP: "remind me to update my password in 7 days"
            if (low.Contains("remind me to") || low.Contains("remind me about") ||
                low.Contains("set a reminder") || low.Contains("set reminder"))
            {
                string title = GetTaskTitle(low);
                int days = GetDays(low);
                string dueDate = days > 0 ? DateTime.Now.AddDays(days).ToString("MMMM dd yyyy") : "No specific date";

                if (title == "")
                    title = "General reminder";

                _db.AddTask(title, "", dueDate, "pending");
                _log.Add("Reminder set: " + title + " for " + dueDate);

                return "Reminder set for '" + title + "' on " + dueDate + ".";
            }

            // interest detection from part 2
            if (low.Contains("interested in") || low.Contains("i like") || low.Contains("i care about"))
            {
                string topic = GetInterest(low);

                if (topic != "")
                {
                    _profile.SavedTopic = topic;
                    _profile.Remember("interest", topic);
                    _log.Add("Interest saved: " + topic);

                    string answer = _topics.FindAnswer(topic);
                    recentTopic = topic;

                    if (answer != "")
                        return "I will remember you are interested in " + topic + ".\n\n" + answer;

                    return "I will remember you are interested in " + topic + ".";
                }
            }

            // tell me more - from part 2
            foreach (string phrase in continueWords)
            {
                if (low.Contains(phrase) && recentTopic != "")
                    return _profile.BuildIntro() + _topics.FindAnswer(recentTopic);
            }

            // mood and topic detection from part 2
            UserMood mood = _mood.CheckMood(low);
            string moodReply = _mood.MoodReply(mood);
            string topicReply = _topics.FindAnswer(low);

            if (topicReply != "")
            {
                recentTopic = _topics.FindKeyword(low);
                _log.Add("Topic answered: " + recentTopic);

                if (moodReply != "")
                    return moodReply + "\n\n" + topicReply;

                return _profile.BuildIntro() + topicReply;
            }

            if (moodReply != "")
            {
                recentTopic = "general";
                return moodReply + "\n\n" + _topics.RandomTip();
            }

            // small talk from part 2
            if (low.Contains("how are you") || low.Contains("how r u"))
                return "I am running perfectly and ready to help you, " + _profile.DisplayName + "!";

            if (low.Contains("help") || low.Contains("what can you do") || low.Contains("topics"))
                return BuildHelpList();

            if (low.Contains("purpose") || low.Contains("what are you"))
                return "My purpose is to help you stay safe online, " + _profile.DisplayName + ".";

            if (low.Contains("hello") || low.Contains("hi") || low.Contains("hey"))
                return "Hello, " + _profile.DisplayName + "! How can I help with cybersecurity today?";

            // fallback
            return PickFallback();

        }//end of HandleMessage


        // pulls a task title from a sentence like "add task enable 2fa"
        private string GetTaskTitle(string input)
        {//start of GetTaskTitle

            string[] markers = {
                "add task to ", "add a task to ", "add task called ",
                "add a task called ", "create task to ", "new task called ",
                "remind me to ", "remind me about ", "set a reminder for ",
                "set reminder for ", "i need to do ",
                "add task ", "add a task "
            };

            foreach (string marker in markers)
            {
                int pos = input.IndexOf(marker);

                if (pos >= 0)
                {
                    string after = input.Substring(pos + marker.Length).Trim().TrimEnd('.', '!', '?');

                    if (after != "")
                    {
                        // capitalise first letter
                        return char.ToUpper(after[0]) + after.Substring(1);
                    }
                }
            }

            return string.Empty;

        }//end of GetTaskTitle


        // pulls a number of days from phrases like "in 7 days" or "tomorrow"
        private int GetDays(string input)
        {//start of GetDays

            if (input.Contains("tomorrow"))
                return 1;

            Match m = Regex.Match(input, @"in\s+(\d+)\s+day");
            if (m.Success)
                return int.Parse(m.Groups[1].Value);

            m = Regex.Match(input, @"(\d+)\s+day");
            if (m.Success)
                return int.Parse(m.Groups[1].Value);

            m = Regex.Match(input, @"in\s+(\d+)\s+week");
            if (m.Success)
                return int.Parse(m.Groups[1].Value) * 7;

            return 0;

        }//end of GetDays


        // pulls the topic from sentences like "i am interested in phishing"
        private string GetInterest(string input)
        {//start of GetInterest

            string[] markers = { "interested in", "i like", "i care about" };

            foreach (string marker in markers)
            {
                int pos = input.IndexOf(marker);

                if (pos >= 0)
                {
                    string after = input.Substring(pos + marker.Length).Trim().TrimEnd('.', '!', '?');
                    if (after != "")
                        return after;
                }
            }

            return string.Empty;

        }//end of GetInterest


        // builds the help list shown to the user
        private string BuildHelpList()
        {//start of BuildHelpList

            string list = "Here is what you can ask me about, " + _profile.DisplayName + ":\n\n" +
                          "  CYBERSECURITY TOPICS\n" +
                          "  - Passwords\n" +
                          "  - Phishing and scams\n" +
                          "  - Privacy\n" +
                          "  - Safe browsing\n" +
                          "  - Malware and viruses\n" +
                          "  - VPN\n" +
                          "  - Firewalls\n" +
                          "  - Two-factor authentication\n" +
                          "  - Fraud\n\n" +
                          "  TASK ASSISTANT\n" +
                          "  - 'add task (name)'      - save a task\n" +
                          "  - 'view tasks'           - see all tasks\n" +
                          "  - 'complete task (id)'   - mark as done\n" +
                          "  - 'delete task (id)'     - remove a task\n\n" +
                          "  QUIZ\n" +
                          "  - 'start quiz'           - test your knowledge\n\n" +
                          "  ACTIVITY LOG\n" +
                          "  - 'show log'             - see recent actions\n\n" +
                          "  After any tip, type 'tell me more' for another on the same topic.";

            if (_profile.SavedTopic != "")
                list += "\n\nI remember you are interested in " + _profile.SavedTopic + ".";

            return list;

        }//end of BuildHelpList


        // builds a string showing all tasks from the database
        private string ShowTasks()
        {//start of ShowTasks

            List<TaskItem> tasks = _db.GetAllTasks();

            if (tasks.Count == 0)
                return "You have no saved tasks. Type 'add task' to create one.";

            string result = "Your cybersecurity tasks:\n\n";

            foreach (TaskItem t in tasks)
            {
                result += "  #" + t.Id + " [" + t.Status.ToUpper() + "] " + t.Name + "\n";

                if (t.Desc != "")
                    result += "     " + t.Desc + "\n";

                result += "     Due: " + t.Due + "\n\n";
            }

            result += "Type 'complete task (id)' or 'delete task (id)' to manage them.";

            return result;

        }//end of ShowTasks


        // returns a random fallback message
        private string PickFallback()
        {//start of PickFallback

            string[] options =
            {
                "I am not sure I understand that, " + _profile.DisplayName + ". Could you rephrase? Type 'help' to see all options.",
                "That is outside what I know about. Ask me about cybersecurity topics, type 'start quiz', or say 'add task'.",
                "I did not quite catch that. Type 'help' to see everything I can assist with.",
                "I am not able to answer that just yet. Ask me about online safety or try 'start quiz'."
            };

            Random rng = new Random();
            return options[rng.Next(options.Length)];

        }//end of PickFallback


    }//end of class

}//end of namespace