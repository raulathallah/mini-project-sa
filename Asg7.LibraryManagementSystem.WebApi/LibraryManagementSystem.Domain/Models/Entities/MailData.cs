using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{
    public class MailData

    {
        public List<string> EmailToIds { get; set; } = null!;
        public List<string> EmailCCIds { get; set; } = null!;
        //public List<string> Attachments { get; set; }
        //public IFormFileCollection Attachments { get; set; }
        public string EmailSubject { get; set; } = null!;
        public string EmailBody { get; set; } = null!;

    }
}
