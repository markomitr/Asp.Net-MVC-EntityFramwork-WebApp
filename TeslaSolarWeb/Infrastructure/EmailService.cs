using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using TeslaSolarWeb.ViewModel;
namespace TeslaSolarWeb.Infrastructure
{
    public class EmailService
    {

        public static bool SendEmail(ref EmailMsg msg)
        {
            bool uspeh = false;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-----------------------------------");
            sb.AppendLine("This message is recived via Web, FROM visitor - (" + msg.Email + ")");
            sb.AppendLine("----------------------------------");
            sb.AppendLine("");
            sb.AppendLine(msg.Message);
            try
            {
                NetworkCredential uplogin = new NetworkCredential("noreply@teslasolarsolutions.com", "A1teslasolar.");
                SmtpClient smtp = new SmtpClient("mail.teslasolarsolutions.com");
                smtp.Credentials = uplogin;
               

                MailMessage eMessage = new MailMessage();
                eMessage.From = new MailAddress("noreply@teslasolarsolutions.com");
                eMessage.Bcc.Add(new MailAddress("teslasolartechnologies@yahoo.com "));
                eMessage.To.Add(new MailAddress("info@teslasolarsolutions.com"));
                eMessage.CC.Add(new MailAddress(msg.Email));
                eMessage.Subject = "TeslaWeb.Visitor - " + msg.Subject;

                eMessage.Body = sb.ToString();

                smtp.Send(eMessage);
                uspeh = true;
                    }
            catch (Exception ex)
            { msg.ErrorMsg = ex.Message + " : " + ex.StackTrace ; }
            return uspeh;
        }
    }
}