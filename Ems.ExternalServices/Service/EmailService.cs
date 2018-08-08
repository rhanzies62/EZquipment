using Ems.Domain.Common;
using Ems.ExternalServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;

namespace Ems.ExternalServices
{
    public class EmailService : IEmailService
    {
        private Response SendEmail(EmailContent content)
        {
            Response response;
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("tblfrancis.cebu@gmail.com", "Blackhackers1991"),
                    EnableSsl = true
                };
                var mailMessage = new System.Net.Mail.MailMessage(new MailAddress("tblfrancis.cebu@gmail.com", "Ezquipment"),new MailAddress(content.To));
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = content.Body;
                mailMessage.Subject = content.Subject;
                client.Send(mailMessage);
                response = new Response(ResponseType.Success);
            }
            catch (Exception e)
            {
                response = new Response(ResponseType.Error, e.GetBaseException().Message);
            }
            return response;
        }

        public Response SendEmailActivation(string fullName, string validationLink, string email)
        {
            var content = EmsExternalServiceResource.validateemailtemplate
                            .Replace(EmsExternalServiceResource.TokenFullName, fullName)
                            .Replace(EmsExternalServiceResource.TokenValidationLink, validationLink);

            var response = this.SendEmail(new EmailContent
            {
                Body = content,
                Subject = EmsExternalServiceResource.SubjectValidateEmail,
                To = email
            });
            return response;
        }
    }

    public class EmailContent
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
