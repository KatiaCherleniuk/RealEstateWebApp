using Microsoft.Extensions.Configuration;
using RealEstateWebApp.DataAccess.Repositories.Stats;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.DataAccess.UserComments
{
    public class UserCommentsRepository : DataController, IUserCommentsRepository
    {
        public UserCommentsRepository(IConfiguration configuration) : base(configuration, "UserComments")
        {
        }

        public Task<bool> DeleteCommentById(int id)
        {
            return DeleteByIdAsync(id);
        }

        public Task<IEnumerable<UserCommentModel>> GetUsersCommentByRecordId(int id)
        {
            return GetManyAsync<UserCommentModel>("GetUsersCommentByRecordId", new { RecordId = id });
        }

        public Task<IEnumerable<UserCommentModel>> GetAllUsersComments()
        {
            return GetManyAsync<UserCommentModel>("GetAllUsersComments");
        }

        public Task<bool> SaveUserComment(UserCommentModel model)
        {
            return InsertWithIdAsync(model);
        }
    }
}
