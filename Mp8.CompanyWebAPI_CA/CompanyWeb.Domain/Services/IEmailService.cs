using CompanyWeb.Domain.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompanyWeb.Domain.Services
{
    public interface IEmailService
    {
        bool SendMail(MailData mailData);
    }
}
