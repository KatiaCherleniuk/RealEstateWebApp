using RealEstateWebApp.Models;
using RealEstateWebApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.DataAccess.UserComments
{
    public interface IUserCommentsRepository
    {
        Task<bool> SaveUserComment(UserCommentModel model);
        Task<IEnumerable<UserCommentModel>> GetAllUsersComments();
        Task<IEnumerable<UserCommentModel>> GetUsersCommentByRecordId(int id);
        Task<bool> DeleteCommentById(int id);
    }
}
