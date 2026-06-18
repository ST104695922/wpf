using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    internal class QuizEngine
    {//start of class

        private List<string> questions = new List<string>();
        private List<string> answers = new List<string>();
        private List<string> explanations = new List<string>();
        private List<bool> isTrueFalse = new List<bool>();

        private int currentIndex = 0;
        private int score = 0;
        public bool IsActive = false;


        public QuizEngine()
        {//start of constructor
            BuildQuestions();
        }//end of constructor


        public string Start()
        {//start of Start

            IsActive = true;
            currentIndex = 0;
            score = 0;

            return "Quiz started! " + questions.Count + " questions.\n\n" + ShowQuestion();

        }//end of Start


        // checks the usersreturns
        public string CheckAnswer(string input)
        {//start of CheckAnswer

            if (currentIndex >= questions.Count)
                return ShowFinalScore();

            string low = input.ToLower().Trim();
            bool correct = false;
            string feedback = "";

            if (isTrueFalse[currentIndex])
            {
                if (low == "true" || low == "t")
                    correct = answers[currentIndex] == "true";
                else if (low == "false" || low == "f")
                    correct = answers[currentIndex] == "false";
                else
                    return "Please type True or False.\n\n" + ShowQuestion();
            }
            else
            {
                // multiple choice question
                if (low == "a" || low == "1") correct = answers[currentIndex] == "a";
                else if (low == "b" || low == "2") correct = answers[currentIndex] == "b";
                else if (low == "c" || low == "3") correct = answers[currentIndex] == "c";
                else if (low == "d" || low == "4") correct = answers[currentIndex] == "d";
                else
                    return "Please answer with A, B, C, or D.\n\n" + ShowQuestion();
            }

            if (correct)
            {
                score++;
                feedback = "Correct! " + explanations[currentIndex];
            }
            else
            {
                feedback = "Incorrect. " + explanations[currentIndex];
            }

            currentIndex++;

            if (currentIndex >= questions.Count)
                return feedback + "\n\n" + ShowFinalScore();

            return feedback + "\n\n" + ShowQuestion();

        }//end of CheckAnswer


        private string ShowQuestion()
        {//start of ShowQuestion

            string text = "Question " + (currentIndex + 1) + " of " + questions.Count + ":\n";
            text += questions[currentIndex];

            if (isTrueFalse[currentIndex])
                text += "\n\nType True or False";

            return text;

        }//end of ShowQuestion


        // shows the final score
        private string ShowFinalScore()
        {//start of ShowFinalScore

            IsActive = false;

            int total = questions.Count;
            int pct = (score * 100) / total;

            string msg = "";

            if (pct >= 90)
                msg = "Outstanding! You are a cybersecurity expert!";
            else if (pct >= 70)
                msg = "Great job! You have a solid understanding of cybersecurity.";
            else if (pct >= 50)
                msg = "Not bad! Keep learning to improve your awareness.";
            else
                msg = "Keep studying. Cybersecurity knowledge is essential for staying safe online.";

            return "Quiz complete!\nYour score: " + score + " out of " + total + " (" + pct + "%)\n\n" + msg;

        }//end of ShowFinalScore

        private void BuildQuestions()
        {//start of BuildQuestions

            questions.Add("What should you do if you receive an email asking for your password?\n" +
                          "A) Reply with your password\n" +
                          "B) Delete the email\n" +
                          "C) Report the email as phishing\n" +
                          "D) Ignore it");
            answers.Add("c");
            explanations.Add("Legitimate organisations never ask for your password by email. Always report phishing.");
            isTrueFalse.Add(false);

            questions.Add("Which of the following is the strongest password?\n" +
                          "A) password123\n" +
                          "B) MyDog2010!\n" +
                          "C) T#9kL!mQ2@vZ\n" +
                          "D) 123456");
            answers.Add("c");
            explanations.Add("A strong password is long and uses uppercase, lowercase, numbers, and symbols.");
            isTrueFalse.Add(false);

            questions.Add("True or False: Using the same password on multiple sites is safe if the password is strong.");
            answers.Add("false");
            explanations.Add("False. If one site is breached, attackers try that password everywhere. Always use unique passwords.");
            isTrueFalse.Add(true);

            questions.Add("What does HTTPS in a website address mean?\n" +
                          "A) The site is popular\n" +
                          "B) Your connection is encrypted\n" +
                          "C) The site is free\n" +
                          "D) The site is government-owned");
            answers.Add("b");
            explanations.Add("HTTPS means your connection to the site is encrypted, protecting your data.");
            isTrueFalse.Add(false);

            questions.Add("True or False: Public Wi-Fi is safe to use for online banking.");
            answers.Add("false");
            explanations.Add("False. Public Wi-Fi is often unencrypted. Always use a VPN on public networks.");
            isTrueFalse.Add(true);

            questions.Add("What is two-factor authentication?\n" +
                          "A) Logging in twice with the same password\n" +
                          "B) A second verification step beyond your password\n" +
                          "C) Using two different browsers\n" +
                          "D) Encrypting a file twice");
            answers.Add("b");
            explanations.Add("2FA requires a second proof so attackers cannot log in with just your password.");
            isTrueFalse.Add(false);

            questions.Add("Which is a common sign of a phishing email?\n" +
                          "A) It comes from a known contact\n" +
                          "B) It uses urgent or threatening language\n" +
                          "C) It has no links\n" +
                          "D) It was sent during business hours");
            answers.Add("b");
            explanations.Add("Phishing emails create urgency to trick you into acting without thinking.");
            isTrueFalse.Add(false);

            questions.Add("True or False: Keeping your software updated helps protect against malware.");
            answers.Add("true");
            explanations.Add("True. Updates include security patches that fix vulnerabilities attackers could use.");
            isTrueFalse.Add(true);

            questions.Add("What is ransomware?\n" +
                          "A) Software that speeds up your computer\n" +
                          "B) Malware that encrypts files and demands payment\n" +
                          "C) A type of antivirus program\n" +
                          "D) A browser extension");
            answers.Add("b");
            explanations.Add("Ransomware locks your files and demands payment to restore access.");
            isTrueFalse.Add(false);

            questions.Add("What is the safest way to check a suspicious link?\n" +
                          "A) Click it to see where it goes\n" +
                          "B) Hover over it to preview the URL\n" +
                          "C) Forward it to a friend\n" +
                          "D) Paste it into a search engine");
            answers.Add("b");
            explanations.Add("Hovering shows the real URL without visiting it so you can spot anything suspicious.");
            isTrueFalse.Add(false);

            questions.Add("Which best describes a VPN?\n" +
                          "A) A type of antivirus software\n" +
                          "B) A service that encrypts traffic and hides your IP\n" +
                          "C) A tool for speeding up downloads\n" +
                          "D) A firewall for your browser");
            answers.Add("b");
            explanations.Add("A VPN encrypts your internet traffic and routes it through a secure server.");
            isTrueFalse.Add(false);

            questions.Add("True or False: A firewall helps block unauthorised access to your computer.");
            answers.Add("true");
            explanations.Add("True. A firewall monitors network traffic and blocks connections that break your security rules.");
            isTrueFalse.Add(true);

        }//end of BuildQuestions


    }//end of class

}//end of namespace