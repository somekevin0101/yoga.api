using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YogaApi.Core.Models;

namespace YogaApi.Core.Interfaces
{
    public interface ISequenceRepository
    {
        Task<long> SaveSequenceData(Sequence sequence);
        Task<SequencePose> SavePoseData(long sequenceId, PoseOrder pose);
        Task SaveMiniSequencePose(long poseSequenceIds, MiniPose pose);
        Task<IEnumerable<SequencePose>> GetSequencePoses(long sequenceId);
        Task<IEnumerable<MiniPose>> GetMiniPoses(long sequencePosesId);
    }
}
