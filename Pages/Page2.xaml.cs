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
            string mail = Global.LogedUser?.Email ?? null;
            string mailPass = Global.LogedUser.EmailPass;
            InitializeComponent();
            Email email = new Email();
            List<Email> mails = email.LoadEmails(mail,mailPass);
            EmailListView1.ItemsSource = mails;
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
