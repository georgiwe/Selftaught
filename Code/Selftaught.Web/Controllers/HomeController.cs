using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using Selftaught.Data;
using Selftaught.Data.Models;
using Selftaught.Web.Models;
using Selftaught.Data.Common.Repositories;

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

        public HomeController()
            : this(new DeletableEntityRepository<Word>(new ApplicationDbContext()))
        {
        }

        public ActionResult CreateWord(WordViewModel newWord)
        {
            return null;

            //var user = this.db.Users.Find(this.User.Identity.GetUserId());
            ////var langName = (string)this.HttpContext.Session[LanguageSessionKey];
            ////var language = this.db.Languages
            ////    .FirstOrDefault(l => l.Name == langName);

            //var language = new Language { Name = "German" };

            //var word = new Word
            //{
            //    AddedByUser = user,
            //    Language = language,
            //    LastPracticed = DateTime.Now,
            //    Name = newWord.Name,
            //    PartOfSpeech = newWord.PartOfSpeech,
            //    Translations = newWord.Translations
            //};

            //this.db.Words.Add(word);
            //this.db.SaveChanges();

            //return this.RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var allWords = this.words.All();

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