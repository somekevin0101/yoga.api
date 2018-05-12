using AutoMapper;
using System;
using YogaApi.Core.Models;
using YogaApi.Models;

namespace YogaApi.Maps
{
    public class UserMaps : Profile
    {
        public UserMaps()
        {
            CreateMap<User, UserGetModel>();
            CreateMap<UserPostModel, User>(MemberList.Source);
        }
    }
}