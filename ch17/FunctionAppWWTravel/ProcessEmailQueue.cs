using System;
using System.Net;
using System.Net.Mail;
using FunctionAppWWTravel.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppWWTravel
{
    public class ProcessEmailQueue
    {

        private readonly ILogger _logger;

        public ProcessEmailQueue(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProcessEmailQueue>();
        }

        [Function("ProcessEmailQueue")]
        public void Run([QueueTrigger("email", Connection = "EmailQueueConnectionString")] EMailData mail, FunctionContext context)
        {
            SendEmail(mail);
            _logger.LogInformation($"Success sending data: {mail}");
        }
        private void SendEmail(EMailData email)
        {
            Helper helper = new Helper();
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
                    if (!String.IsNullOrEmpty(email.To))
                    {
                        mailMessage.To.Add(email.To);
                        mailMessage.Subject = email.Subject;
                        mailMessage.Body = email.Body;
                        smtp.Send(mailMessage);
                    }
                }
            }
        }
    }
}
