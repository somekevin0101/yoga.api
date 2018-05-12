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
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("yogaapi/api/v1/users/{userId}")]
        public async Task<IHttpActionResult> GetUser(int userId)
        {
            ApiResponse<UserGetModel> response = await _usersService.GetUser(userId);
            if (response.httpStatusCode == (int)HttpStatusCode.NotFound) return Content(HttpStatusCode.NotFound, response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("yogaapi/api/v1/users")]
        public async Task<IHttpActionResult> CreateUser(UserPostModel user)
        {
            ApiResponse<int> response = await _usersService.CreateUser(user);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("yogaapi/api/v1/users/{userId}/sequences")]
        public async Task<IHttpActionResult> GetShallowSequences(int userId)
        {
            ApiResponse<IEnumerable<SequenceGetModel>> response = await _usersService.GetShallowSequences(userId);
            return Ok(response);
        }
    }
}