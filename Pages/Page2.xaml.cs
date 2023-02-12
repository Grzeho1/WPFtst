using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using Org.BouncyCastle.Asn1.Tsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

using System.Text;
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
using static MailKit.Net.Imap.ImapMailboxFilter;

namespace WPFtest2.Pages
{
    /// <summary>
    /// Interakční logika pro Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            LoadEmails();

        }

        private List<Email> emails = new List<Email>();

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmails();
        }

        private int emailId = 0;

        private void LoadEmails()
        {

            using (ImapClient client = new ImapClient())
            {
                client.Connect("imap.seznam.cz", 993, true);
                client.Authenticate("testtest139@seznam.cz", "testujuaplikaci");
                IMailFolder inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                int messageCount = inbox.Count;
                int startIndex = messageCount - 30;
                if (startIndex < 0)
                {
                    startIndex = 0;
                }
                var uids = inbox.Search(SearchQuery.All);
                foreach (UniqueId uid in uids.Reverse().Take(30).Reverse())
                {
                    MimeMessage message = inbox.GetMessage(uid);
                    emails.Add(new Email
                    {

                        Id = emailId++,
                        Sender = message.From.ToString(),
                        Subject = message.Subject,
                        Date = message.Date.ToLocalTime().DateTime,
                        Body = message.TextBody,
                    }) ; 
                }
                client.Disconnect(true);
            }

            EmailListView1.ItemsSource = emails;

        }
       
       

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            MessagePopup.IsOpen = false;
        }

        private void EmailListView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedEmail = EmailListView1.SelectedItem as Email;

            if (selectedEmail != null)
            {
                // Display the message content in the TextBlock
                MessageTextBlock.Text = selectedEmail.Body;

                // Show the pop-up
                MessagePopup.IsOpen = true;
            }
        }
    }

       
    
    
       
}
