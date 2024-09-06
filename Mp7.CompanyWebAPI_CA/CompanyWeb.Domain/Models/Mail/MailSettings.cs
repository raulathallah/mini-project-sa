using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CompanyWeb.Domain.Models.Mail
{
    public class MailSettings

    {
        public string EmailId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }

    }
}
