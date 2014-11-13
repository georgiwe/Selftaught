namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Selftaught.Data.DataAccess;

    public class UsersController : BaseController
    {
        public UsersController(ISelftaughtData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public PartialViewResult GetLatestUsers()
        {
            var latestUsers = this.data.Users.All()
                .OrderByDescending(u => u.CreatedOn)
                .Take(6);

            return PartialView("_LatestUsersPartial", latestUsers);
        }
    }
}