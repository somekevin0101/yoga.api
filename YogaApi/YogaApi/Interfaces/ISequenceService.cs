using System;
using System.Threading.Tasks;
using YogaApi.Core.Models;
using YogaApi.Models;

namespace YogaApi.Interfaces
{
    public interface ISequenceService
    {
        Task<ApiResponse<long>> SaveSequence(SequencePostModel model);
        Task<ApiResponse<SequencePosesGetModel>> GetSequencePoses(long sequenceId);
    }
}
