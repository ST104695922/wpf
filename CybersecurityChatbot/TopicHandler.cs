using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
   
     
        internal class TopicHandler
        {//start of class

            private Dictionary<string, List<string>> _answer_bank;
            private Random _rng = new Random();


            public TopicHandler()
            {//start of constructor
                _answer_bank = new Dictionary<string, List<string>>();
                BuildAnswers();
            }//end of constructor


            public string FindAnswer(string input)
            {//start of FindAnswer

                string lowered = input.ToLower();

                foreach (string topic in _answer_bank.Keys)
                {
                    if (lowered.Contains(topic))
                    {
                        List<string> options = _answer_bank[topic];
                        return options[_rng.Next(options.Count)];
                    }
                }

                return string.Empty;

            }//end of FindAnswer


            public string FindKeyword(string input)
            {//start of FindKeyword

                string lowered = input.ToLower();

                foreach (string topic in _answer_bank.Keys)
                {
                    if (lowered.Contains(topic))
                        return topic;
                }

                return string.Empty;

            }//end of FindKeyword


            public List<string> ListTopics()
            {//start of ListTopics
                return new List<string>(_answer_bank.Keys);
            }//end of ListTopics


            public string RandomTip()
            {//start of RandomTip

                List<string> all_keys = new List<string>(_answer_bank.Keys);
                string picked_key = all_keys[_rng.Next(all_keys.Count)];
                List<string> options = _answer_bank[picked_key];
                return options[_rng.Next(options.Count)];

            }//end of RandomTip


            private void BuildAnswers()
            {//start of BuildAnswers

                // ─ PASSWORD 
                _answer_bank["password"] = new List<string>
            {
                "A strong password should be at least twelve characters long and include a combination of uppercase letters, lowercase letters, numbers, and special symbols. The length of a password is one of the most important factors in how difficult it is for attackers to crack.",
                "You should never use the same password for more than one account. If an attacker gains access to one of your accounts, they will try that same password on all your other accounts. A free password manager like Bitwarden can generate and store a unique password for every site you use.",
                "Avoid including personal information in your passwords such as your name, date of birth, or the name of a pet. Attackers who research you online can guess this information and use it to break into your accounts.",
                "A passphrase is a sequence of three or four random words joined together, which makes it both long and easier to remember than a short complex password. A passphrase is significantly harder for automated tools to crack than a typical short password.",
                "If you receive a notification that one of your accounts appeared in a data breach, change that password immediately. You can check whether your email address has been exposed in a breach by visiting the website haveibeenpwned.com."
            };

                // ─ PHISHING 
                _answer_bank["phishing"] = new List<string>
            {
                "Phishing is a type of attack where criminals send messages that appear to come from a trusted source, such as your bank or a well-known company, in order to trick you into revealing your personal information or login credentials.",
                "Before you click any link in an email or message, hover your mouse pointer over it to see the actual web address it leads to. If the address looks unfamiliar, contains misspellings, or does not match the organisation it claims to be from, do not click it.",
                "Legitimate organisations such as banks, government departments, or reputable companies will never ask you to confirm your password, one-time PIN, or banking details through an email or SMS message.",
                "Be alert to warning signs in suspicious messages such as spelling errors, urgency in the language used, threats about account suspension, or greetings that use 'Dear Customer' instead of your actual name.",
                "If you are unsure whether a message is genuine, contact the organisation directly by typing their official website address into your browser yourself rather than using any links in the message."
            };

                // ─ SCAM 
                _answer_bank["scam"] = new List<string>
            {
                "Scammers use urgency and pressure to prevent you from thinking clearly before acting. Whenever you receive an unexpected request for personal information or money, take your time to verify it independently before responding.",
                "Any online offer that promises an unusually large reward, prize, or investment return in exchange for a small upfront payment is almost certainly a scam. Legitimate competitions and investments do not require you to pay anything to claim a reward.",
                "Never transfer money, send gift cards, or make cryptocurrency payments to a person you have only communicated with online, regardless of how convincing or sympathetic their circumstances appear to be.",
                "If you believe you have been targeted by a scam, report it to your bank if money was involved, and file a report with the South African Police Service cybercrime unit to help prevent others from being victimised."
            };

                // ─ PRIVACY 
                _answer_bank["privacy"] = new List<string>
            {
                "Take the time to review the privacy settings on all your social media profiles and limit who can see your personal information, your photos, your location, and your daily activity. Many platforms are set to share more information publicly by default than most users realise.",
                "Be thoughtful about the details you share online, even when they seem harmless individually. Information such as your workplace, your neighbourhood, your daily routine, and your travel plans can be combined by attackers to build an accurate profile of you.",
                "Consider using a privacy-focused web browser such as Brave or Mozilla Firefox together with privacy extensions to reduce the amount of data that advertising networks and trackers collect about your browsing habits.",
                "Check the permissions granted to the applications on your smartphone regularly. Many applications request access to your camera, microphone, contacts, and precise location even when this access is not necessary for the application to function.",
                "When you use a shared or public computer, always use a private or incognito browsing window. This prevents your passwords, browsing history, and session cookies from being stored on that device after you leave."
            };

                // ─ BROWSING 
                _answer_bank["browsing"] = new List<string>
            {
                "Before entering any personal or financial information on a website, check that the web address begins with HTTPS and that there is a padlock icon displayed in the browser's address bar. This confirms that your connection to the website is encrypted.",
                "Always keep your web browser and any extensions or plugins you have installed updated to their latest versions. Software updates regularly include patches for security vulnerabilities that could otherwise be exploited by attackers.",
                "Only download software, applications, and media files from official and trusted sources. Unofficial download sites frequently bundle malware alongside the files they offer, which can infect your device without your knowledge.",
                "Installing a reputable advertisement blocker such as uBlock Origin in your browser helps protect you from malicious advertisements, which can sometimes install malware on your device simply by being displayed on a page."
            };

                // ─ MALWARE 
                _answer_bank["malware"] = new List<string>
            {
                "Install reliable antivirus or anti-malware software on all your devices and configure it to update its virus definitions automatically. Keeping the software up to date ensures it can detect and remove the latest known threats.",
                "Email attachments are one of the most common ways that malware is delivered to victims. Never open an attachment from a sender you do not recognise, and be cautious even with attachments from known senders if the message seems unexpected or unusual.",
                "Keeping your operating system and all installed applications updated is one of the most effective steps you can take to protect yourself from malware, since the majority of attacks target vulnerabilities that have already been fixed in newer versions.",
                "Backing up your important files regularly to an external hard drive or a cloud storage service protects you in the event of a ransomware infection, where your files could be encrypted and held hostage by attackers demanding payment."
            };

                // ─ VIRUS 
                _answer_bank["virus"] = new List<string>
            {
                "If your device begins running unusually slowly, displaying unexpected pop-up messages, or behaving in ways that seem abnormal, run a full scan using your antivirus software immediately to check whether it has been infected.",
                "Computer viruses are typically spread through infected file attachments, malicious software downloads, and compromised external storage devices such as USB drives. Exercise caution with all of these sources.",
                "Make sure your antivirus software is configured to update its virus definition database automatically and regularly. Definitions that are out of date may fail to detect newer threats that have been discovered since the last update."
            };

                // ─ VPN 
                _answer_bank["vpn"] = new List<string>
            {
                "A Virtual Private Network, commonly known as a VPN, works by encrypting all data sent and received between your device and the internet and routing it through a secure server. This makes it very difficult for third parties to monitor or intercept your online activity.",
                "You should always use a VPN when connecting to Wi-Fi networks in public places such as coffee shops, airports, hotels, and shopping centres, as these networks are frequently monitored by attackers looking to intercept unencrypted data.",
                "When selecting a VPN service, prioritise providers that publish independently verified no-logs policies, meaning they do not keep records of your online activity. ProtonVPN is a well-regarded option that offers a free tier for personal use.",
                "Using a VPN replaces your real IP address with the address of the VPN server, which makes it significantly more difficult for websites, advertisers, and other parties to track your location and monitor your browsing behaviour across different sites."
            };

                // ─ WIFI 
                _answer_bank["wifi"] = new List<string>
            {
                "Public Wi-Fi networks in places like coffee shops, airports, and hotels are a favourite target for attackers because the traffic on these networks is often unencrypted and easy to monitor. Always use a VPN when connecting to public Wi-Fi.",
                "Avoid accessing sensitive services such as your online banking, email, or work applications when connected to a public Wi-Fi network without the protection of a VPN.",
                "For your home network, ensure that your router is configured to use WPA2 or WPA3 encryption and is protected by a strong password that is different from the factory default that came with the device."
            };

                // ─ FIREWALL 
                _answer_bank["firewall"] = new List<string>
            {
                "A firewall is a security system that examines network traffic entering and leaving your device or network and blocks connections that do not comply with your security rules. It acts as a first line of defence against unauthorised access.",
                "Ensure that the built-in firewall on your device is always switched on. This is one of the simplest and most effective basic security measures available and should never be disabled unless there is a specific and temporary reason to do so.",
                "In addition to the software firewall built into your device's operating system, enabling the firewall feature on your home router provides an additional layer of protection for every device connected to your home network."
            };

                // ─ TWO-FACTOR AUTHENTICATION 
                _answer_bank["two-factor"] = new List<string>
            {
                "Two-factor authentication is a security feature that requires you to provide a second piece of verification in addition to your password when logging into an account. Even if an attacker has obtained your password, they cannot log in without this second factor.",
                "Using an authenticator application such as Google Authenticator or Authy to generate your verification codes is more secure than receiving codes by SMS, because SMS-based codes can be intercepted through a technique known as SIM swapping.",
                "Your email account should be the first account on which you enable two-factor authentication, since your email address is used to reset the passwords of nearly all your other online accounts and is therefore the most valuable account for an attacker to compromise.",
                "Physical security keys such as a YubiKey device provide the strongest available form of two-factor authentication for everyday users and offer excellent protection against phishing attacks that attempt to steal your login codes."
            };

                _answer_bank["2fa"] = _answer_bank["two-factor"];
                _answer_bank["authentication"] = _answer_bank["two-factor"];

                // ─ FRAUD 
                _answer_bank["fraud"] = new List<string>
            {
                "If you notice any transactions on your bank account or credit card that you did not authorise, contact your bank immediately using the official number on the back of your card. Most banks have a dedicated fraud reporting line available around the clock.",
                "Financial fraud should be reported to your bank as soon as possible and, if a criminal act has taken place, to the South African Police Service. Acting quickly gives you the best chance of recovering any funds that were taken.",
                "Reviewing your bank and credit card statements at least once a week helps you spot unauthorised charges early. Fraudsters often start with small test transactions to verify that a stolen card works before attempting larger withdrawals."
            };

                // ─ HACKED 
                _answer_bank["hacked"] = new List<string>
            {
                "If you suspect that one of your accounts has been accessed without your permission, change the password immediately and use the platform's security settings to sign out of all active sessions on all devices.",
                "Contact the support team for the affected platform as soon as possible and explain what has happened. Be prepared to verify your identity by providing account details or answering security questions to prove that you are the legitimate account owner.",
                "Once you have regained control of a compromised account, enable two-factor authentication straight away to significantly reduce the risk of it being accessed without your permission again in the future."
            };

                // ─ CYBERSECURITY 
                _answer_bank["cybersecurity"] = new List<string>
            {
                "Cybersecurity refers to the practice of protecting computers, networks, systems, and the data stored on them from unauthorised access, theft, and damage caused by digital attacks. It is a concern for every person who uses a device or connects to the internet.",
                "Practising good cybersecurity habits on a daily basis, such as using strong unique passwords, enabling two-factor authentication, keeping your software updated, and being cautious about what you click, significantly reduces your exposure to online threats.",
                "Cybersecurity is not only a concern for large organisations. Individual users are frequently targeted by attackers specifically because personal devices and home networks tend to have fewer security protections in place than those used in businesses."
            };

            }//end of BuildAnswers


        }//end of class

    }//end of namespace
