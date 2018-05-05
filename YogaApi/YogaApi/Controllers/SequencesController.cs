using System;
using System.Threading.Tasks;
using System.Web.Http;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Controllers
{
    public class SequencesController : ApiController
    {
        private readonly ISequencesService _sequencesService;

        public SequencesController(ISequencesService sequencesService)
        {
            _sequencesService = sequencesService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("yogaapi/api/v1/sequences")]
        public async Task<IHttpActionResult> SaveSequence(SequencePostModel model)
        {
            return Ok(await _sequencesService.SaveSequence(model));
        }
    }
}