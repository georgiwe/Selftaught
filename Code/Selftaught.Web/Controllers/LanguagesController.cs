namespace Selftaught.Web.Controllers
{
    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class LanguagesController : Controller
    {
        private IRepository<Language> languages;

        public LanguagesController(IRepository<Language> languages)
        {
            this.languages = languages;
        }

        [ChildActionOnly]
        public PartialViewResult GetLanguageNames()
        {
            var languageNames = this.languages.All()
                .Select(l => l.Name)
                .ToList();

            return PartialView("_ChangeLanguagePartial", languageNames);
        }

        //[ValidateAntiForgeryToken]
        public ActionResult ChangeLanguage(string langName, string returnUrl)
        {
            var newLang = this.languages.All().FirstOrDefault(l => l.Name == langName);
            this.Session["language"] = newLang.Name;

            return this.Redirect(returnUrl);
        }
    }
}