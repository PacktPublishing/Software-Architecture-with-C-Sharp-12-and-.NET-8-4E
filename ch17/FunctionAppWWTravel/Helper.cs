using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppWWTravel
{
    class Helper
    {
        public string SmtpServer => Environment.GetEnvironmentVariable("SmtpServer", EnvironmentVariableTarget.Process);
        public string SmtpUser => Environment.GetEnvironmentVariable("SmtpUser", EnvironmentVariableTarget.Process);
        public string SmtpPassword => Environment.GetEnvironmentVariable("SmtpPassword", EnvironmentVariableTarget.Process);
        public string SmtpDomain => Environment.GetEnvironmentVariable("SmtpDomain", EnvironmentVariableTarget.Process);
        public int SmtpPort => Convert.ToInt32(Environment.GetEnvironmentVariable("SmtpPort", EnvironmentVariableTarget.Process));
    }
}
