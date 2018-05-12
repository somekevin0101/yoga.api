using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YogaApi.Core.Models;
using YogaApi.Models;

namespace YogaApi.Interfaces
{
    public interface IUsersService
    {
        Task<ApiResponse<UserGetModel>> GetUser(int userId);
        Task<ApiResponse<int>> CreateUser(UserPostModel user);
        Task<ApiResponse<IEnumerable<SequenceGetModel>>> GetShallowSequences(int userId);
    }
}
