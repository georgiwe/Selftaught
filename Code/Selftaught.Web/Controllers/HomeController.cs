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
using Selftaught.Web.ViewModels.Words;

namespace Selftaught.Web.Controllers
{
    public class HomeController : Controller
    {
        protected const string LanguageSessionKey = "Language";
        protected IRepository<Word> words;

        public HomeController(IRepository<Word> wordsDb)
        {
            this.words = wordsDb;
        }

        public ActionResult CreateWord(WordViewModel newWord)
        {
            return null;
        }

        public ActionResult Index()
        {
            var allWords = this.words.All()
                .Project()
                .To<WordDetailedViewModel>();

            return View(allWords);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}