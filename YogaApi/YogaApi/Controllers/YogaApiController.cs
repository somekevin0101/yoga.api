using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using YogaApi.Interfaces;
using YogaApi.Services.LevelOne;

namespace YogaApi.Controllers
{
    public abstract class YogaApiController : ApiController
    {

        public IUserService GetUserService(IEnumerable<IUserService> userServices)
        {
            string type = HttpContext.Current.Request.Headers.Get("UserType");
            bool success = Enum.TryParse(type, true, out UserType userType);
            if (!success) throw new ArgumentException("Invalid User Type");

            switch (userType)
            {
                case UserType.Regular:
                    return userServices.OfType<UserService>().First();
                case UserType.Super:
                    return userServices.OfType<SuperUserService>().First();
                default: throw new ArgumentException("Invalid User Type");
            }
        }
    }

    public enum UserType
    {
        Regular = 1,
        Super
    }
}