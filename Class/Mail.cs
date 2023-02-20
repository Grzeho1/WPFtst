using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Controls.Primitives;

namespace WPFtest2.Class
{
    public class Mail
    {
        
            private List<Mail> emails = new List<Mail>();
        private int emailId = 0;


        public string Sender { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }

        public int Unread { get; set; }

        // Metoda pro načtení mailů   ***?? Vyřešit dlouhe načítání pri klik na zalozku Mail ??***
        #region
        public List<Mail> LoadEmails(string mail, string mailPass)
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
                        var message = inbox.GetMessage(uid);
                        var summary = inbox.Fetch(new[] { uid }, MessageSummaryItems.Flags).First();
                        emails.Add(new Mail
                        {

                            Id = emailId++,
                            Sender = message.From.ToString(),
                            Subject = message.Subject,
                            Date = message.Date.ToLocalTime().DateTime,
                            Body = message.TextBody,
                            IsRead = !summary.Flags.HasValue || summary.Flags.Value.HasFlag(MessageFlags.Seen)
                            

                        }) ; 
                        Global.Emails=emails; // naplneni seznamu emailu do globálni tridy
                    }
                    client.Disconnect(true);
                }
                return emails;
            }
            catch (Exception ex)
            {
                return new List<Mail>();
            }
        }
        #endregion

        // Oznaceni mailu jako přectený

        public void MarkAsRead(string mail, string mailPass,string Body)

        {
            using (ImapClient client = new ImapClient())
            {

                client.Connect("imap.seznam.cz", 993, true);
                client.Authenticate(mail, mailPass); ;


                IMailFolder inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);

                var uids = inbox.Search(SearchQuery.BodyContains(Body));
                inbox.AddFlags(uids, MessageFlags.Seen, true);


            }


        }






    }


}
