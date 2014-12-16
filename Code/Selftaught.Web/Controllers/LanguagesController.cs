namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using AutoMapper.QueryableExtensions;

    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.DataAccess;
    using Selftaught.Data.Models;
    using Selftaught.Web.ViewModels.Language;

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

        [HttpGet]
        public ActionResult Manage()
        {
            var availableLanguages = GetAllAvailableLanguages();
            return View(availableLanguages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(IEnumerable<int> selectedLanguageIds)
        {
            var availableLanguages = GetAllAvailableLanguages();

            if (selectedLanguageIds == null || ModelState.IsValid == false)
            {
                this.TempData["error"] = "Please select at least one language.";
                return RedirectToAction("Manage");
            }

            var user = this.GetCurrentUser();

            user.StudyingLanguages.Clear();
            this.data.SaveChanges();

            foreach (var langId in selectedLanguageIds)
            {
                var lang = this.data.Languages.Find(langId);

                if (lang == null)
                {
                    this.TempData["error"] = "Language does not exist.";
                    return RedirectToAction("Manage");
                }

                user.StudyingLanguages.Add(lang);
            }

            this.data.SaveChanges();
            this.Session["language"] = user.StudyingLanguages.FirstOrDefault().Name;
            this.TempData["success"] = "Languages Updated!";

            return View(availableLanguages);
        }

        private IEnumerable<LanguageViewModel> GetAllAvailableLanguages()
        {
            var availableLanguages = this.data.Languages.All()
                .Project()
                .To<LanguageViewModel>();

            return availableLanguages;
        }
    }
}