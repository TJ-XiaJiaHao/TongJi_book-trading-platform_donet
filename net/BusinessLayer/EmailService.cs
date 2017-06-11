using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace net.BusinessLayer
{
    public class EmailService
    {
        private EmailService() { }
        /// <summary>
        /// 通过smtp服务将给定信息以给定标题发送到目的邮箱
        /// </summary>
        /// <param name="messBody"></param>
        /// <param name="subject"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void sendEmail(string messBody, string subject, string from, string to)
        {
            MailMessage mess = new MailMessage();
            mess.Body = messBody;
            mess.Subject = subject;
            mess.From = new MailAddress(from);
            mess.To.Add(new MailAddress(to));

            SmtpClient smtp = new SmtpClient();
            smtp.Send(mess);
        }
    }
}