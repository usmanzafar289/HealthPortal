using System.Net;
using System.Net.Mail;

namespace CarePortal.API.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmail(EmailInfo emailInfo)
        {
            if (!string.IsNullOrWhiteSpace(emailInfo.SenderEmail) && !string.IsNullOrWhiteSpace(emailInfo.SenderEmailPassword))
            {
                var fromAddress = new MailAddress(emailInfo.SenderEmail, "no reply");
                var toAddress = new MailAddress(emailInfo.RecipientEmail, emailInfo.RecipientName);
                var smtp = new SmtpClient
                {
                    Host = emailInfo.EmailServer,
                    Port = emailInfo.Port,
                    EnableSsl = emailInfo.EnableSSL,
                    UseDefaultCredentials = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(emailInfo.SenderEmail, emailInfo.SenderEmailPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = emailInfo.Subject,
                    Body = emailInfo.Body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class EmailInfo
    {
        public string EmailServer { get; set; }
        public string SenderEmail { get; set; }
        public string RecipientEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public string To { get; set; }
        public string RecipientName { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }
}
