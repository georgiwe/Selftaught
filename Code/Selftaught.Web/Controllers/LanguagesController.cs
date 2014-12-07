namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.DataAccess;
    using Selftaught.Data.Models;

    public class LanguagesController : BaseController
    {
        private const string LanguageNotFoundMsg = "The specified language was not found.";

        public LanguagesController(ISelftaughtData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public PartialViewResult GetLanguageNames()
        {
            IEnumerable<string> languageNames = null;

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.Identity.GetUserId();
                var user = this.data.Users.Find(userId);

                languageNames = user.StudyingLanguages
                    .Where(l => l.IsDeleted == false)
                    .Select(l => l.Name);
            }
            else
            {
                languageNames = this.data.Languages.All()
                .Select(l => l.Name)
                .ToList();
            }

            return PartialView("_ChangeLanguagePartial", languageNames);
        }

        public ActionResult ChangeLanguage(string langName, string returnUrl)
        {
            var newLang = this.data.Languages.All()
                .FirstOrDefault(l => l.Name == langName);

            if (newLang == null)
                this.TempData["error"] = LanguageNotFoundMsg;
            else
                this.Session["language"] = newLang.Name;

            return this.Redirect(returnUrl);
        }
    }
}