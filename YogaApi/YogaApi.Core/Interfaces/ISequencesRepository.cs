using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YogaApi.Core.Models;

namespace YogaApi.Core.Interfaces
{
    public interface ISequencesRepository
    {
        Task<long> SaveSequenceData(Sequence sequence);
        Task<SequencePoses> SavePoseData(long sequenceId, PoseOrder pose);
        Task SaveMiniSequencePose(long poseSequenceIds, MiniPose pose);
    }
}
