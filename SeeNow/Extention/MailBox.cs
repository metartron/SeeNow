using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SeeNow
{
    public static partial class MailBox
    {
        public static void SendMail( string account,string toMail,string subject,string body )
        {
            SmtpClient myMail = new SmtpClient("msa.hinet.net");
            MailAddress from = new MailAddress("metartron+seenow@gmail.com", "SeeNow服務中心");
            MailAddress to = new MailAddress(toMail);
            MailMessage Msg = new MailMessage(from, to);
            Msg.Subject = subject;
            Msg.Body = body;

            Msg.IsBodyHtml = true;

            myMail.Send(Msg);
        }

    }
}