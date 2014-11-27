using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper.QueryableExtensions;

using Selftaught.Web.ViewModels.Language;
using Selftaught.Data.DataAccess;
using Selftaught.Data.Models;
using Kendo.Mvc.UI;
using AutoMapper;

namespace Selftaught.Web.Areas.Administration.Controllers
{
    public class LanguagesController : KendoGridAdminController
    {
        public LanguagesController(ISelftaughtData data)
            : base(data)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        }

        protected override System.Collections.IEnumerable GetData()
        {
            var langs = this.data.Languages.All()
                .Project()
                .To<LanguageViewModel>();
            return langs;
        }

        protected override T GetById<T>(object id)
        {
            return this.data.Languages.Find(id) as T;
        }

        // GET: Administration/Languages
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, LanguageViewModel model)
        {
            var dbModel = base.Create<Language>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, LanguageViewModel model)
        {
            //if (model.Id == null)
            //    this.Create(request, model);
            //else
            base.Update<Language, LanguageViewModel>(model, model.Id);

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, LanguageViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var lang = this.data.Languages.Find(model.Id.Value);

                foreach (var wordId in lang.Words.Select(w => w.Id).ToList())
                {
                    this.data.Words.Delete(wordId);
                }

                this.data.Languages.Delete(lang);
                this.data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}