using AutoMapper;
using System;
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

        public async Task<ApiResponse<string>> SaveSequence(SequencePostModel model)
        {
            string sequenceId = await _sequencesRepository.SaveSequence(_mapper.Map<Sequence>(model)).ConfigureAwait(false);
            return new ApiResponse<string>(sequenceId, HttpStatusCode.OK, true);
        }
    }
}