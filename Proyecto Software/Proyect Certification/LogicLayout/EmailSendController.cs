using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using EntidadesLayout.Email;

namespace LogicLayout
{
    public class EmailSendController
    {
        public bool SendEMail(Email objEmail)
        {
            var IsSend = false;
            var smtp = new SmtpClient();
            var mailSystem = System.Configuration.ConfigurationManager.AppSettings["EmailSystem"];
            var passEmail = System.Configuration.ConfigurationManager.AppSettings["PassSystem"];
            var host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = host;
            smtp.Port = 25;
            smtp.EnableSsl = true;
            smtp.Timeout = 10000;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mailSystem, passEmail);

            var email = new MailMessage(objEmail.From, objEmail.To, objEmail.Subject, objEmail.Body);

            if (objEmail.Cc != null) email.CC.Add(objEmail.Cc);
            if (objEmail.Cco != null) email.Bcc.Add(objEmail.Cco);

            email.IsBodyHtml = true; 
            email.Priority = MailPriority.Normal;

            try
            {
                IsSend = true; 
                smtp.Send(email);
            }
            catch (Exception)
            {
                IsSend = false;
            }

            return IsSend;
        }
    }
}
