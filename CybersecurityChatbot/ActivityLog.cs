using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    internal class ActivityLog
    {//start of class

        private List<string> log = new List<string>();


        // adds an action to log 
        public void Add(string action)
        {//start of Add

            string entry = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + action;
            log.Add(entry);

        }//end of Add


        // returns the  10 log 
        public string GetLog()
        {//start of GetLog

            if (log.Count == 0)
                return "No actions have been recorded yet.";

            string result = "Here is a summary of recent actions:\n\n";

            // show only 10 entries
            int start = 0;
            if (log.Count > 10)
                start = log.Count - 10;

            int count = 1;
            for (int i = log.Count - 1; i >= start; i--)
            {
                result += count + ". " + log[i] + "\n";
                count++;
            }

            if (log.Count > 10)
                result += "\nType 'show full log' to see all " + log.Count + " entries.";

            return result;

        }//end of GetLog


        // returns 
        public string GetFullLog()
        {//start of GetFullLog

            if (log.Count == 0)
                return "No actions have been recorded yet.";

            string result = "Full activity log:\n\n";

            int count = 1;
            for (int i = log.Count - 1; i >= 0; i--)
            {
                result += count + ". " + log[i] + "\n";
                count++;
            }

            return result;

        }//end of GetFullLog


    }//end of class

}//end of namespace