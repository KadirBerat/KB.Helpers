using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;

namespace KB.Helpers.ClassLibrary
{
    class MailHelper
    {
        public static async Task CustomErrorMailSend(string message, string subject, string mailAddress, string password, List<string> recipients)
        {
            if ((message != null || message != "") && (subject != null || subject != "") && (mailAddress != null ||mailAddress != "") && (password != null || password != "") && recipients.Count != 0)
            {
                MailMessage eMail = new MailMessage();
                eMail.From = new MailAddress(mailAddress);
                foreach (var recipient in recipients)
                {
                    string rcp = recipient.ToString();
                    if (rcp.Contains("@") == true && rcp.Contains(".com"))
                    {
                        eMail.To.Add(rcp);
                    }
                }
                eMail.Subject = subject;
                eMail.Body = message;
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential(mailAddress, password);
                smtp.Port = 587;
                smtp.Host = "smtp.yandex.com.tr";
                smtp.EnableSsl = true;
                try
                {
                    await Task.Run(() => smtp.SendAsync(eMail, null));
                }
                catch (Exception)
                {
                    try
                    {
                        await Task.Run(() => smtp.SendAsync(eMail, null));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error", ex);
                    }
                }
            }
        }
    }
}
