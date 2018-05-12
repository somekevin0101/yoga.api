using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YogaApi.Core.Models;

namespace YogaApi.Core.Interfaces
{
    public interface IUsersRepository
    {
        Task<int> CreateUser(User user);
        Task<User> GetUser(int userId);
        Task<IEnumerable<ShallowSequence>> GetShallowSequences(int userId);
    }
}
