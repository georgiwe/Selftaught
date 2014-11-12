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

namespace Selftaught.Web.Controllers
{
    public class HomeController : Controller
    {
        protected IRepository<Word> words;

        public HomeController(IRepository<Word> words)
        {
            this.words = words;
        }

        public ActionResult Index()
        {
            this.Session["language"] = "German";

            var rnd = new Random();

            var topWords = this.words.All()
                .OrderBy(r => Guid.NewGuid())
                .Project()
                .To<WordDetailedViewModel>()
                .Take(6);

            var vm = new HomePageDataViewModel
            {
                TopWords = topWords
            };

            return View(vm);
        }
    }
}