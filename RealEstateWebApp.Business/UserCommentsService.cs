using Microsoft.Extensions.Logging;
using RealEstateWebApp.DataAccess.Repositories.Records;
using RealEstateWebApp.DataAccess.UserComments;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.RecordViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Business
{
    public class UserCommentsService
    {
        private readonly IUserCommentsRepository _userCommentsRepository;
        public UserCommentsService(IUserCommentsRepository userCommentsRepository)
        {
            _userCommentsRepository = userCommentsRepository;
        }
        public async Task SaveUserComment(UserCommentModel userCommentModel)
        {
            await _userCommentsRepository.SaveUserComment(userCommentModel);
        }
    }
}
