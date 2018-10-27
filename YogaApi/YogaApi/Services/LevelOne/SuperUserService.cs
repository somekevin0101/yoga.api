using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using YogaApi.Core.Models;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Services.LevelOne
{
    public class SuperUserService : IUserService
    {
        public Task<ApiResponse<int>> CreateUser(UserPostModel user)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<SequenceGetModel>>> GetShallowSequences(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserGetModel>> GetUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}