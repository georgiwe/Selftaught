namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;

    using Selftaught.Data.DataAccess;

    public class UsersController : BaseController
    {
        private const int NumOfLatestUsers = 5;
        private const int MinutesToCacheLatestUsers = 5;
        private const string LatestUsers = "latestUsers";

        public UsersController(ISelftaughtData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public PartialViewResult GetLatestUsers()
        {
            this.GetUsersAndCacheIfNotCached();
            var latestUsers = HttpContext.Cache[LatestUsers];
            return PartialView("_LatestUsersPartial", latestUsers);
        }

        [NonAction]
        private void GetUsersAndCacheIfNotCached()
        {
            if (HttpContext.Cache[LatestUsers] != null) return;

            var latestUsers = this.data.Users.All()
                .OrderByDescending(u => u.CreatedOn)
                .Take(NumOfLatestUsers)
                .Select(u => u.UserName)
                .ToList();

            this.HttpContext.Cache.Add(LatestUsers, latestUsers, null,
                DateTime.Now.AddMinutes(MinutesToCacheLatestUsers), 
                TimeSpan.Zero, CacheItemPriority.Default, null);
        }
    }
}