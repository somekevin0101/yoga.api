using AutoMapper;
using System;
using YogaApi.Core.Models;
using YogaApi.Models;

namespace YogaApi.Maps
{
    public class SequenceMaps : Profile
    {
        public SequenceMaps()
        {
            CreateMap<SequencePose, PoseOrderGetModel>(MemberList.Source);
            CreateMap<MiniPose, MiniPoseOrderApiModel>();
        }
    }
}