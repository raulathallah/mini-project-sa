using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Mappers
{
    public static class UserMappers
    {
        public static UserDetailResponse ToUserResponse (this User userModel)
        {
            return new UserDetailResponse()
            {
                FName = userModel.FName,
                LName = userModel.LName,
                UserId = userModel.UserId,
                AppUserId = userModel.AppUserId,
                UserPrivilage = userModel.UserPrivilage,
                UserPosition = userModel.UserPosition,
                UserNotes = userModel.UserNotes,
                LibraryCardExpiredDate = userModel.LibraryCardExpiredDate,
                LibraryCardNumber = userModel.LibraryCardNumber,
            };
        }
    }
}
