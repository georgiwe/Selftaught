using Selftaught.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Selftaught.Web.Controllers
{
    public class WordController : BaseController
    {
        public WordController(ISelftaughtData data)
            :base(data)
        {
        }

        // GET: Word/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}