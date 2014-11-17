namespace Selftaught.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using Selftaught.Data.DataAccess;
    using Selftaught.Data.Models;
    using Selftaught.Web.ViewModels.Words;

    public class WordsController : KendoGridAdminController
    {
        public WordsController(ISelftaughtData data)
            : base(data)
        {
        }

        protected override IEnumerable GetData()
        {
            var words = this.data.Words.All()
                .Project()
                .To<WordDetailedViewModel>();

            return words;
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Words.Find(id) as T;
        }

        // GET: Administration/Words
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, WordDetailedViewModel model)
        {
            var dbModel = base.Create<Word>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }
    }
}