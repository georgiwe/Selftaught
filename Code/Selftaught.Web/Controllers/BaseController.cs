using Selftaught.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Selftaught.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ISelftaughtData data;

        public BaseController(ISelftaughtData data)
        {
            this.data = data;
        }
    }
}