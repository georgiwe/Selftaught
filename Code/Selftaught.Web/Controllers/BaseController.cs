using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Selftaught.Data.DataAccess;
using Selftaught.Data.Models;

namespace Selftaught.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ISelftaughtData data;

        public BaseController(ISelftaughtData data)
        {
            this.data = data;
        }

        protected ApplicationUser GetCurrentUser()
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.Find(userId);

            return user;
        }
    }
}