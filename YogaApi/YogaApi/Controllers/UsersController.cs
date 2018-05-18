using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using YogaApi.Core.Models;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Gets user information by user id
        /// </summary>
        /// <param name="userId">user id (int32)</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "user returned", typeof(ApiResponse<UserGetModel>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "user not found")]
        [Route("yogaapi/api/v1/users/{userId}")]
        public async Task<IHttpActionResult> GetUser(int userId)
        {
            ApiResponse<UserGetModel> response = await _userService.GetUser(userId);
            if (response.httpStatusCode == (int)HttpStatusCode.NotFound) return Content(HttpStatusCode.NotFound, response);
            return Ok(response);
        }
        /// <summary>
        /// Saves a user returning a user id (int32)
        /// </summary>
        /// <param name="user"> user model</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "user saved", typeof(ApiResponse<int>))]
        [Route("yogaapi/api/v1/users")]
        public async Task<IHttpActionResult> CreateUser(UserPostModel user)
        {
            ApiResponse<int> response = await _userService.CreateUser(user);
            return Ok(response);
        }
        /// <summary>
        /// Gets all sequence ids with basic information for a user id
        /// </summary>
        /// <param name="userId">user id (int32)</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "user sequences returned", typeof(ApiResponse<IEnumerable<SequenceGetModel>>))]
        [Route("yogaapi/api/v1/users/{userId}/sequences")]
        public async Task<IHttpActionResult> GetShallowSequences(int userId)
        {
            ApiResponse<IEnumerable<SequenceGetModel>> response = await _userService.GetShallowSequences(userId);
            return Ok(response);
        }
    }
}