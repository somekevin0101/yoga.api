using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Services.LevelOne
{
    public class SequenceService : ISequenceService
    {
        private readonly ISequenceRepository _sequenceRepository;
        private readonly IMapper _mapper;

        public SequenceService(ISequenceRepository sequenceRepository, IMapper mapper)
        {
            _sequenceRepository = sequenceRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<long>> SaveSequence(SequencePostModel model)
        {
            var sequence = _mapper.Map<Sequence>(model);
            long sequenceId = await _sequenceRepository.SaveSequenceData(sequence).ConfigureAwait(false);
            List<SequencePose> miniSequences = await SavePoses(sequenceId, sequence.Poses);
            if (miniSequences.Any()) await SaveMiniSequences(miniSequences, sequence.Poses);
            return new ApiResponse<long>(sequenceId, HttpStatusCode.OK, true);
        }

        private async Task<List<SequencePose>> SavePoses(long sequenceId, List<PoseOrder> poses)
        {
            List<SequencePose> miniSequences = new List<SequencePose>();
            List<Task<SequencePose>> sequencePosesTasks = new List<Task<SequencePose>>();

            foreach (PoseOrder pose in poses)
            {
                sequencePosesTasks.Add(_sequenceRepository.SavePoseData(sequenceId, pose));
            }

            SequencePose[] allPoses = await Task.WhenAll(sequencePosesTasks).ConfigureAwait(false);

            foreach (SequencePose model in allPoses)
            {
                if (model.IsMiniSequence)
                {
                    miniSequences.Add(model);
                }
            }

            return miniSequences;
        }

        private async Task SaveMiniSequences(List<SequencePose> poseSequenceIds, List<PoseOrder> poses)
        {
            List<Task> miniSequenceTasks = new List<Task>();
            foreach (PoseOrder model in poses)
            {
                if (model.IsMiniSequence)
                {
                    foreach (MiniPose pose in model.MiniSequence)
                    {
                        var sequencePose = poseSequenceIds.Single(r => r.OrderInSequence == model.OrderInSequence);
                        miniSequenceTasks.Add(_sequenceRepository.SaveMiniSequencePose(sequencePose.SequencePosesId, pose));
                    }
                }
            }

            await Task.WhenAll(miniSequenceTasks).ConfigureAwait(false);
        }

        public async Task<ApiResponse<SequencePosesGetModel>> GetSequencePoses(long sequenceId)
        {
            SequencePosesGetModel model = new SequencePosesGetModel(sequenceId);
            List<SequencePose> sequencePoses = await _sequenceRepository.GetSequencePoses(sequenceId).ConfigureAwait(false) as List<SequencePose>;
            for (int i = 0; i < sequencePoses.Count; i ++ )
            {
                SequencePose pose = sequencePoses[i];
                model.Poses.Add(_mapper.Map<PoseOrderGetModel>(pose));
                if (pose.IsMiniSequence)
                {
                    List<MiniPose> miniPoses = await _sequenceRepository.GetMiniPoses(pose.SequencePosesId).ConfigureAwait(false) as List<MiniPose>;
                    model.Poses[i].MiniSequence = _mapper.Map<List<MiniPoseOrderApiModel>>(miniPoses);
                }
            }

            return new ApiResponse<SequencePosesGetModel>(model, HttpStatusCode.OK, true);
        }
    }
}