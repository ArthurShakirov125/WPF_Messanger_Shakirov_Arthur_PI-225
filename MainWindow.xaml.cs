using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MessangerProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string UserName;
        private ClientServerPart _client;
        private MyMessage messageForChat;


        public MainWindow()
        {
            InitializeComponent();
            UserName = UsernameUI.Text;
            _client = new ClientServerPart(UserName);
        }

        private void Run()
        {
            messageForChat = _client.RecieveMessage();
            TextBlock textBlock = new TextBlock()
            {
                Text = $"{messageForChat.Time.ToString("HH:mm")} {messageForChat.UserName}:{messageForChat.MessageText}",
                TextWrapping = TextWrapping.Wrap,
            };

            MessagesField.Children.Add(textBlock);

        }

        private void Messadge_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = $"[{DateTime.Now.ToString("HH:mm")}] {UserName} : {MessageText.Text}",
                TextWrapping = TextWrapping.Wrap,
            };

            MessagesField.Children.Add(textBlock);
            MessageText.Text = "";
        }


        private void EnterMessage(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                _client.SendMessage(MessageText.Text);
                MessageText.Text = "";
            }

        }

        private void Edit_Username(object sender, MouseButtonEventArgs e)
        {
            EditUserNameField.Visibility = Visibility.Visible;
            EditUserNameField.Focus();
        }

        private void Edit(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                _client.UserName = UsernameUI.Text;
                UsernameUI.Text = EditUserNameField.Text;
                UserName = UsernameUI.Text;
                EditUserNameField.Text = "";
                EditUserNameField.Visibility = Visibility.Hidden;
            }
        }
    }
}
