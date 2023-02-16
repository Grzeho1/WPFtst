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
    public partial class Page2
    {

        private class Email
        {
            private List<Email> emails = new List<Email>();
            private int emailId = 0;


            public string Sender { get; set; }
            public string Subject { get; set; }
            public DateTime Date { get; set; }
            public int Id { get; set; }
            public string Body { get; set; }


            // Metoda pro načtení mailů
            #region
            public List<Email> LoadEmails(string mail,string mailPass)
            {

                try
                {
                    using (ImapClient client = new ImapClient())
                    {

                        client.Connect("imap.seznam.cz", 993, true);
                        client.Authenticate(mail, mailPass); ;


                        IMailFolder inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadOnly);
                        int messageCount = inbox.Count;
                        int startIndex = messageCount - 30;

                        if (startIndex < 0)
                        {
                            startIndex = 0;
                        }

                        var uids = inbox.Search(SearchQuery.All);
                        foreach (UniqueId uid in uids.Reverse().Take(30))
                        {
                            MimeMessage message = inbox.GetMessage(uid);
                            emails.Add(new Email
                            {

                                Id = emailId++,
                                Sender = message.From.ToString(),
                                Subject = message.Subject,
                                Date = message.Date.ToLocalTime().DateTime,
                                Body = message.TextBody,
                            });
                        }
                        client.Disconnect(true);
                    }
                    return emails;
                }
                catch (Exception ex)
                {
                    return new List<Email>();
                }
                }
            #endregion

        }





    }
    
       
}
