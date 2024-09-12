using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface IPdfService
    {
        public byte[] OnGeneratePDF(string htmlcontent);
    }
}
