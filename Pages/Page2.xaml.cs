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
using WPFtest2.Class;
using System.Security.Cryptography.X509Certificates;

namespace WPFtest2.Pages
{
    /// <summary>
    /// Interakční logika pro Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        string Bmail = Global.LogedUser?.Email ?? null;
        string mailPass = Global.LogedUser?.EmailPass ?? null;
        


        public Page2()
        {
            
            InitializeComponent();
            Mail Newemail = new Mail();
            List<Mail> mails = Newemail.LoadEmails(Bmail, mailPass);
            EmailListView1.ItemsSource = mails;
             


            //Vraci počet neprečtených mailů
            
            if (Global.LogedUser != null)
            {
                int unreadCount = mails != null ? mails.Count(email => !email.IsRead) : 0;
                Global.LogedUser.Unread = unreadCount;
            }
            else
            {
                Global.LogedUser = null;
            }

        }
       
        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            MessagePopup.IsOpen = false;
        }

        private void EmailListView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var selectedEmail = EmailListView1.SelectedItem as Mail;
            EmailBodyTextBox.Text = selectedEmail.Body;

            if (selectedEmail != null)
            {
                MessagePopup.IsOpen = true;
            }

            selectedEmail.MarkAsRead(Bmail, mailPass,selectedEmail.Id);

            

        }

        
    }
}

       
    
    
       

