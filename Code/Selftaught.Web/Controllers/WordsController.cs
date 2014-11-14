namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.DataAccess;
    using Selftaught.Data.Models;
    using Selftaught.Web.InputModels.Words;
    using Selftaught.Web.ViewModels.Words;

    public class WordsController : BaseController
    {
        public WordsController(ISelftaughtData data)
            : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add(string name)
        {
            return View(new WordInputModel() { Name = name });
        }

        public ActionResult Learn()
        {
            var currLang = Session["language"].ToString().ToLower();

            var words = this.data.Words.All()
                .Where(w => w.AddedByUser == null)
                .Where(w => w.Language.Name.ToLower() == currLang)
                .OrderBy(w => Guid.NewGuid())
                .Take(5)
                .Project()
                .To<WordShortViewModel>()
                .ToList();

            return View(words);
        }

        [ChildActionOnly]
        public PartialViewResult GetTopWords()
        {
            if (this.HttpContext.Cache["topwords"] == null)
            {
                var currLang = this.Session["language"].ToString().ToLower();

                var words = this.data.Words.All()
                    .Where(w => w.Language.Name.ToLower() == currLang)
                    .OrderBy(r => Guid.NewGuid())
                    .Project()
                    .To<WordDetailedViewModel>()
                    .Take(6)
                    .ToList();

                this.HttpContext.Cache.Add(
                    "topwords", words, null,
                    DateTime.Now.AddHours(24),
                    TimeSpan.Zero,
                    CacheItemPriority.Default, null);
            }

            var topWords = this.HttpContext.Cache["topwords"];
            return PartialView("_TopWordsPartial", topWords);
        }
    }
}