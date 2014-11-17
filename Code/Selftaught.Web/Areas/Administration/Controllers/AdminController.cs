using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Selftaught.Data.DataAccess;
using Selftaught.Web.Controllers;

namespace Selftaught.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(ISelftaughtData data)
            : base(data)
        {
        }


    }
}