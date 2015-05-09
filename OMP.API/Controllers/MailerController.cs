using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Web.Http;

namespace OMP.API.Controllers
{
    [RoutePrefix("api/Mailmaster")]
    public class MailerController : ApiController
    {
        [Route("Send")]
        public HttpResponseMessage SendEmail()
        {

                try
                {
                    SendMail();
                    return Request.CreateResponse(HttpStatusCode.Created, true);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            
        }
        public void SendMail()
        {
            // Gmail Address from where you send the mail
            var fromAddress = "cwc2015.southtech@gmail.com";
            // any address where the email will be sending
            var toAddress = "monirulmask@gmail.com";
            //Password of your gmail address
            const string fromPassword = "%basis2015%";
            // Passing the values and make a email formate to display
            string subject = "This is Test Email";
            string body = "From: Monirul Islam\n";
            body += "Email: cwc2015.southtech@gmail.com \n";
            body += "Subject: Test Email\n";
            body += "Message: \n Testing \n";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                // smtp.Port = 587;
                smtp.Port = 2525;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                // request.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }

        



    }
}
