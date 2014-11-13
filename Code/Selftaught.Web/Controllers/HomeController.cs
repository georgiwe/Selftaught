using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using AutoMapper.QueryableExtensions;

using Selftaught.Data;
using Selftaught.Data.Common.Repositories;
using Selftaught.Data.Models;
using Selftaught.Web.InputModels.Words;
using Selftaught.Web.ViewModels.Home;
using Selftaught.Web.ViewModels.Words;
using Selftaught.Data.DataAccess;

namespace Selftaught.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISelftaughtData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            if (this.Session["language"] == null)
            {
                this.Session["language"] = "German";
            }

            return View();
        }
    }
}