using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Services.LevelOne
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateUser(UserPostModel user)
        {
            int userId = await _usersRepository.CreateUser(_mapper.Map<User>(user)).ConfigureAwait(false);
            return new ApiResponse<int>(userId, HttpStatusCode.OK, true);
        }

        public async Task<ApiResponse<IEnumerable<SequenceGetModel>>> GetShallowSequences(int userId)
        {
            IEnumerable<SequenceGetModel> shallowSequences =
                _mapper.Map<IEnumerable<SequenceGetModel>>(await _usersRepository.GetShallowSequences(userId).ConfigureAwait(false));
            return new ApiResponse<IEnumerable<SequenceGetModel>>(shallowSequences, HttpStatusCode.OK, true);
        }

        public async Task<ApiResponse<UserGetModel>> GetUser(int userId)
        {
            User user = await _usersRepository.GetUser(userId).ConfigureAwait(false);
            if (user == null) return new ApiResponse<UserGetModel>(null, HttpStatusCode.NotFound, false);
            return new ApiResponse<UserGetModel>(_mapper.Map<UserGetModel>(user), HttpStatusCode.OK, true);
        }
    }
}