using System;
using System.Threading.Tasks;
using YogaApi.Core.Models;
using YogaApi.Models;

namespace YogaApi.Interfaces
{
    public interface ISequencesService
    {
        Task<ApiResponse<long>> SaveSequence(SequencePostModel model);
    }
}
