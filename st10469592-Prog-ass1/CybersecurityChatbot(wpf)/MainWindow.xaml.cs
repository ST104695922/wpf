using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace CybersecurityChatbot_wpf_
{//start of namespace

    public partial class MainWindow : Window
    {//start of class

        // single instance of the bot engine - all logic lives there
        private BotEngine _bot;


        public MainWindow()
        {//start of constructor

            InitializeComponent();

            // set up the bot engine
            _bot = new BotEngine();

            // AudioHelper carried over from Part 1 - plays greet.wav on startup
            AudioHelper audio = new AudioHelper();
            audio.PlayGreeting();

            // show the opening message
            ShowBotMessage(_bot.OpeningMessage());

            status_bar.Text = "Waiting for name...";

        }//end of constructor


        // send button click
        private void send_btn_Click(object sender, RoutedEventArgs e)
        {//start of send_btn_Click
            SubmitMessage();
        }//end of send_btn_Click


        // enter key shortcut
        private void message_box_KeyDown(object sender, KeyEventArgs e)
        {//start of message_box_KeyDown
            if (e.Key == Key.Enter)
                SubmitMessage();
        }//end of message_box_KeyDown


        // SubmitMessage - reads input box, calls bot engine, displays result
        private void SubmitMessage()
        {//start of SubmitMessage

            string typed = message_box.Text.Trim();

            if (string.IsNullOrWhiteSpace(typed))
            {
                ShowBotMessage("Please type something before sending.");
                return;
            }

            // show the user message
            ShowUserMessage(typed);

            // clear input box
            message_box.Clear();

            // send to bot engine and show the reply
            string reply = _bot.HandleMessage(typed);
            ShowBotMessage(reply);

            // update status bar once name is known
            if (!string.IsNullOrEmpty(_bot.FetchName()))
                status_bar.Text = "Chatting as: " + _bot.FetchName();

        }//end of SubmitMessage


        // ShowBotMessage - adds a pink-bordered bot bubble to the chat list
        private void ShowBotMessage(string text)
        {//start of ShowBotMessage

            Border bubble = new Border
            {
                Margin = new Thickness(0, 3, 60, 3),
                Padding = new Thickness(10, 7, 10, 7),
                CornerRadius = new CornerRadius(6),
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Color.FromRgb(40, 5, 40)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(200, 80, 140))
            };

            TextBlock block = new TextBlock { TextWrapping = TextWrapping.Wrap };

            block.Inlines.Add(new Run
            {
                Text = "ChatBot:  ",
                Foreground = new SolidColorBrush(Color.FromRgb(255, 105, 180)),
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily("Consolas")
            });

            block.Inlines.Add(new Run
            {
                Text = text,
                Foreground = new SolidColorBrush(Color.FromRgb(240, 200, 225)),
                FontFamily = new FontFamily("Consolas")
            });

            bubble.Child = block;
            chat_display.Items.Add(bubble);
            chat_display.ScrollIntoView(chat_display.Items[chat_display.Items.Count - 1]);

        }//end of ShowBotMessage


        // ShowUserMessage - adds a purple-bordered user bubble to the chat list
        private void ShowUserMessage(string text)
        {//start of ShowUserMessage

            string label = string.IsNullOrEmpty(_bot.FetchName()) ? "You" : _bot.FetchName();

            Border bubble = new Border
            {
                Margin = new Thickness(60, 3, 0, 3),
                Padding = new Thickness(10, 7, 10, 7),
                CornerRadius = new CornerRadius(6),
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Color.FromRgb(30, 0, 45)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(130, 60, 180))
            };

            TextBlock block = new TextBlock { TextWrapping = TextWrapping.Wrap };

            block.Inlines.Add(new Run
            {
                Text = label + ":  ",
                Foreground = new SolidColorBrush(Color.FromRgb(200, 140, 255)),
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily("Consolas")
            });

            block.Inlines.Add(new Run
            {
                Text = text,
                Foreground = new SolidColorBrush(Color.FromRgb(220, 200, 240)),
                FontFamily = new FontFamily("Consolas")
            });

            bubble.Child = block;
            chat_display.Items.Add(bubble);
            chat_display.ScrollIntoView(chat_display.Items[chat_display.Items.Count - 1]);

        }//end of ShowUserMessage


    }//end of class

}//end of namespaceespace
