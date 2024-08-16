using LMS.Core.Models.Requests;
using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Factory
{
    public class UserFactory
    {
        public static User CreateUser(AddUserRequest request)
        {
            return new User()
            {
               Username = request.Username,
               Phonenumber = request.Phonenumber
            };
        }
    }
}
