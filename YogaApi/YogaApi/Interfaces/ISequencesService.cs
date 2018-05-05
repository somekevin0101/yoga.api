using System;
using System.Threading.Tasks;
using YogaApi.Core.Models;
using YogaApi.Models;

namespace YogaApi.Interfaces
{
    public interface ISequencesService
    {
        Task<ApiResponse<string>> SaveSequence(SequencePostModel model);
    }
}
