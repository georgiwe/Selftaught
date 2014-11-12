using Selftaught.Data.Common.Repositories;
using Selftaught.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Selftaught.Web.Controllers
{
    public class WordsController : Controller
    {
        protected IRepository<Word> words;

        public WordsController(IRepository<Word> words)
        {
            this.words = words;
        }

        // GET: Words
        public ActionResult Index()
        {
            return View();
        }
    }
}