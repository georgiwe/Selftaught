namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Caching;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Selftaught.Data.DataAccess;
    using Selftaught.Web.ViewModels.Words;
    using Selftaught.Common.Extensions;
    using Selftaught.Data.Models;
    using Selftaught.Data.Factories;
    using Selftaught.Web.InputModels.Words;

    public class WordsController : BaseController
    {
        private const string WrongLanguageMsg = "The specified language is not supported currently.";
        private const string WordCreatedMsg = "Word added successfully!";

        private IWordAttributeFactory wordAttrsFact;

        public WordsController(ISelftaughtData data, IWordAttributeFactory wordAttrsFact)
            : base(data)
        {
            this.wordAttrsFact = wordAttrsFact;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add(string name)
        {
            var user = this.GetCurrentUser();

            ViewBag.Name = name;
            ViewBag.LanguageId = new SelectList(user.StudyingLanguages, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Word newWord,
            IEnumerable<WordAttributeInputModel> attributes,
            IEnumerable<WordTranslationViewModel> translations)
        {
            var user = this.GetCurrentUser();

            newWord = this.BuildWord(newWord, attributes, translations, user);
            this.ClearMeaningErrors(newWord);

            if (!ModelState.IsValid)
            {
                ViewBag.LanguageId = new SelectList(user.StudyingLanguages, "Id", "Name", string.Empty);
                return View(newWord);
            }

            this.data.Words.Add(newWord);
            this.data.SaveChanges();

            TempData["success"] = WordCreatedMsg;
            return RedirectToAction("Add");
        }

        [HttpGet]
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

        [Authorize]
        [HttpGet]
        public PartialViewResult GetAttributesPartial(string lang, string pos)
        {
            if (!this.data.Languages.All().Any(l => l.Name.ToLower() == lang.ToLower()))
            {
                return null;
            }

            var attrs = this.wordAttrsFact.GetAttributes(lang, pos)
                .AsQueryable()
                .Project()
                .To<WordAttributeInputModel>();

            return PartialView("_WordAttributesPartial", attrs);
        }

        protected Word BuildWord(Word word,
            IEnumerable<WordAttributeInputModel> attributes,
            IEnumerable<WordTranslationViewModel> translations,
            ApplicationUser user)
        {
            var lang = Session["language"].ToString().ToLower();

            var language = this.data.Languages.All()
                .FirstOrDefault(l => l.Name.ToLower() == lang);

            word.AddedByUser = user;
            word.LastPracticed = DateTime.Now;

            word.Attributes = attributes.AsQueryable()
                .Where(a => a.Name != null)
                .Project().To<WordAttribute>().ToList();

            word.Translations = translations
                .Where(t => t.Meaning != null)
                .Select(t => new WordTranslation { Language = language, Meaning = t.Meaning })
                .ToList();

            return word;
        }

        protected void ClearMeaningErrors(Word word)
        {
            if (word.Translations.Count > 0)
            {
                var meaningErrors = ModelState.Keys
                    .Where(k => k.Contains("Meaning"))
                    .ForEach(k => ModelState[k].Errors.Clear());
            }

            if (word.Attributes.Count > 0 ||
                word.PartOfSpeech == PartOfSpeech.Adjective)
            {
                var meaningErrors = ModelState.Keys
                    .Where(k => k.Contains("Value") || k.Contains("Name"))
                    .ForEach(k => ModelState[k].Errors.Clear());
            }
        }
    }
}