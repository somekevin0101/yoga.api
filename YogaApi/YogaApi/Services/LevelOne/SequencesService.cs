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
    public class SequencesService : ISequencesService
    {
        private readonly ISequencesRepository _sequencesRepository;
        private readonly IMapper _mapper;

        public SequencesService(ISequencesRepository sequencesRepository, IMapper mapper)
        {
            _sequencesRepository = sequencesRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<long>> SaveSequence(SequencePostModel model)
        {
            var sequence = _mapper.Map<Sequence>(model);
            long sequenceId = await _sequencesRepository.SaveSequenceData(sequence).ConfigureAwait(false);
            List<SequencePoses> miniSequences = await SavePoses(sequenceId, sequence.Poses);
            if (miniSequences.Any()) await SaveMiniSequences(miniSequences, sequence.Poses);
            return new ApiResponse<long>(sequenceId, HttpStatusCode.OK, true);
        }

        private async Task<List<SequencePoses>> SavePoses(long sequenceId, List<PoseOrder> poses)
        {
            List<SequencePoses> miniSequences = new List<SequencePoses>();
            List<Task<SequencePoses>> sequencePosesTasks = new List<Task<SequencePoses>>();

            foreach(PoseOrder pose in poses)
            {
                sequencePosesTasks.Add(_sequencesRepository.SavePoseData(sequenceId, pose));
            }

            SequencePoses[] allPoses = await Task.WhenAll(sequencePosesTasks).ConfigureAwait(false);

            foreach(SequencePoses model in allPoses)
            {
                if (model.IsMiniSequence)
                {
                    miniSequences.Add(model);
                }
            }

            return miniSequences;
        }

        private async Task SaveMiniSequences(List<SequencePoses> poseSequenceIds, List<PoseOrder> poses)
        {
            List<Task> miniSequenceTasks = new List<Task>();
            foreach(PoseOrder model in poses)
            {
                if (model.IsMiniSequence)
                {
                    foreach(MiniPose pose in model.MiniSequence)
                    {
                        var sequencePose = poseSequenceIds.Single(r => r.OrderInSequence == model.OrderInSequence);
                        miniSequenceTasks.Add(_sequencesRepository.SaveMiniSequence(sequencePose.SequencePosesId, pose));
                    }
                }
            }

            await Task.WhenAll(miniSequenceTasks).ConfigureAwait(false);
        }
    }
}