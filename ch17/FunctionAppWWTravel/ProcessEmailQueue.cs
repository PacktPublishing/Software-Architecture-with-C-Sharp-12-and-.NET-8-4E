using System;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using FunctionAppWWTravel.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppWWTravel
{
    public static class ProcessEmailQueue
    {
        [FunctionName("ProcessEmailQueue")]
        public static void Run([QueueTrigger("email", Connection = "EmailQueueConnectionString")] string myQueueItem, ILogger log)
        {
            SendEmail(myQueueItem);
            log.LogInformation($"Success sending data: {myQueueItem}");
        }
        private static void SendEmail(string data)
        {
            Helper helper = new Helper();
            EMailData email = JsonSerializer.Deserialize<EMailData>(data);
            using (MailMessage mailMessage = new MailMessage())
            {
                using (SmtpClient smtp = new SmtpClient(helper.SmtpServer))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Port = helper.SmtpPort;
                    NetworkCredential smtpUserInfo = new NetworkCredential(helper.SmtpUser, 
                        helper.SmtpPassword, helper.SmtpDomain);

                    smtp.Credentials = smtpUserInfo;
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.None;
                    mailMessage.From = new MailAddress(helper.SmtpUser);
                    mailMessage.To.Add(email.To);
                    mailMessage.Subject = email.Subject;
                    mailMessage.Body = email.Body;
                    smtp.Send(mailMessage);
                }
            }
        }
    }
}
