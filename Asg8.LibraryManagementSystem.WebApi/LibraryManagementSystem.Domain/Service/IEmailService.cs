using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface IEmailService
    {
        bool SendMail(MailData mailData);
    }
}
