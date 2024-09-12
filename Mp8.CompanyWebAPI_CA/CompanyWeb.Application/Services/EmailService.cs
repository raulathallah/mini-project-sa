
using CompanyWeb.Domain.Models.Mail;
using CompanyWeb.Domain.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private IHostingEnvironment _env;

        public EmailService(IOptions<MailSettings> mailSettings,
            IHostingEnvironment env)
        {
            _mailSettings = mailSettings.Value;
            _env = env;
        }
        public bool SendMail(MailData mailData)
        {
            var emailMessage = CreateEmailMessage(mailData);
            var result = Send(emailMessage);
            return result;
        }

        private MimeMessage CreateEmailMessage(MailData mailData)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.Subject = mailData.EmailSubject;
            var x = _mailSettings;
            MailboxAddress emailFrom = new MailboxAddress(_mailSettings.Name, _mailSettings.EmailId);
            emailMessage.From.Add(emailFrom);
            BodyBuilder emailBodyBuilder = new BodyBuilder();

            if (mailData.EmailToIds != null && mailData.EmailToIds.Any())
            {
                foreach(var to in  mailData.EmailToIds)
                {
                    MailboxAddress email_To = new MailboxAddress(to,to);
                    emailMessage.To.Add(email_To);
                }
            }
            if (mailData.EmailCCIds != null && mailData.EmailCCIds.Any())
            {
                foreach (var cc in mailData.EmailCCIds)
                {
                    MailboxAddress email_Cc = new MailboxAddress(cc, cc);
                    emailMessage.Cc.Add(email_Cc);
                }
            }
            /*if(mailData.Attachments != null && mailData.Attachments.Any())
            {
                byte[] fileBytes;
                foreach(var a in mailData.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        a.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    emailBodyBuilder.Attachments.Add(a.FileName, fileBytes, ContentType.Parse(a.ContentType)); 
                }
            }*/

            //the file
            /*var byteData = File.ReadAllBytes(@"./Test pdf.pdf");
            emailBodyBuilder.Attachments.Add("Invoice.pdf", byteData);*/
            emailBodyBuilder.HtmlBody = mailData.EmailBody;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();
            return emailMessage;
                 
        }

        private bool Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_mailSettings.Host, _mailSettings.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                    client.Send(mailMessage);
                    return true;
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                    //log an error message or throw an exception or both.
                    return false;

                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }

            }

        }
    }
}
