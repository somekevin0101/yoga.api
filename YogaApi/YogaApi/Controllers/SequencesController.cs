using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using YogaApi.Core.Models;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Controllers
{
    public class SequencesController : ApiController
    {
        private readonly ISequenceService _sequenceService;

        public SequencesController(ISequenceService sequenceService)
        {
            _sequenceService = sequenceService;
        }
        /// <summary>
        /// Saves a yoga sequence returning a sequence id (int64)
        /// </summary>
        /// <param name="model"> sequence model </param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "sequence saved", typeof(ApiResponse<long>))]
        [Route("yogaapi/api/v1/sequences")]
        public async Task<IHttpActionResult> SaveSequence(SequencePostModel model)
        {
            return Ok(await _sequenceService.SaveSequence(model));
        }
        /// <summary>
        /// Returns a yoga sequence 
        /// </summary>
        /// <param name="sequenceId"> the sequence id</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "sequence poses returned", typeof(ApiResponse<SequencePosesGetModel>))]
        [Route("yogaapi/api/v1/sequences/{sequenceId}")]
        public async Task<IHttpActionResult> GetSequencePoses(long sequenceId)
        {
            ApiResponse<SequencePosesGetModel> response = await _sequenceService.GetSequencePoses(sequenceId);
            return Ok(response);
        }
    }
}