namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.DataAccess;
    using Selftaught.Data.Models;
    using Selftaught.Web.ViewModels.Words;

    public class WordsController : BaseController
    {
        public WordsController(ISelftaughtData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult GetTopWords()
        {
            var currLang = this.Session["language"].ToString().ToLower();

            var topWords = this.data.Words.All()
                .Where(w => w.Language.Name.ToLower() == currLang)
                .OrderBy(r => Guid.NewGuid())
                .Project()
                .To<WordDetailedViewModel>()
                .Take(6);

            return PartialView("_TopWordsPartial", topWords);
        }
    }
}