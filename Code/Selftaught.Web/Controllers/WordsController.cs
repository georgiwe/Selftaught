namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Caching;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Selftaught.Common.Extensions;
    using Selftaught.Data.DataAccess;
    using Selftaught.Data.Models;
    using Selftaught.Data.Factories;
    using Selftaught.Web.InputModels.Words;
    using Selftaught.Web.Infrastructure.CustomAttributes;
    using Selftaught.Web.ViewModels.Words;

    public class WordsController : BaseController
    {
        private const string WrongLanguageMsg = "The specified language is not supported currently.";
        private const string WordCreatedMsg = "Word added successfully!";
        private const string WordUpdatedMsg = "Word updated successfully";
        private const string ErroneousInput = "Please add at least one translation and all word articles";
        private const string InvalidWordDataMsg = "Invalid word data";

        private IWordAttributeFactory wordAttrsFact;

        public WordsController(ISelftaughtData data, IWordAttributeFactory wordAttrsFact)
            : base(data)
        {
            this.wordAttrsFact = wordAttrsFact;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var user = this.GetCurrentUser();
            var lang = this.GetCurrentLanguage();

            var words = this.data.Words.All()
                .Where(w => w.AddedByUserId == user.Id)
                .Where(w => w.Language.Name.ToLower() == lang)
                .Project()
                .To<WordDetailedViewModel>()
                .ToList();

            return View(words);
        }

        [HttpGet]
        [Authorize]
        [AjaxOnly]
        public PartialViewResult GetWordDetailsPartial(int id)
        {
            var word = this.data.Words.Find(id);
            var model = AutoMapper.Mapper.Map<WordDetailedViewModel>(word);
            return PartialView("_WordDetailsPartial", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Dictionary()
        {
            var user = this.GetCurrentUser();
            var lang = this.GetCurrentLanguage();

            var words = this.data.Words.All()
                .Where(w => w.AddedByUserId == null || w.AddedByUserId == user.Id)
                .Where(w => w.Language.Name.ToLower() == lang)
                .Project()
                .To<WordDetailedViewModel>()
                .ToList();

            return View("Index", words);
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
                TempData["error"] = ErroneousInput;
                ViewBag.LanguageId = new SelectList(user.StudyingLanguages, "Id", "Name", string.Empty);
                return View(newWord);
            }

            this.data.Words.Add(newWord);
            this.data.SaveChanges();

            TempData["success"] = WordCreatedMsg;
            return RedirectToAction("Add");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string wordName)
        {
            ViewBag.WordName = wordName;
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        [Authorize]
        public ActionResult GetEditPartial(string wordName)
        {
            var user = this.GetCurrentUser();
            var word = this.data.Words.All()
                .Where(w => w.AddedByUserId == user.Id)
                .FirstOrDefault(w => w.Name.ToLower() == wordName.ToLower());

            if (word == null)
            {
                return HttpNotFound();
            }

            ViewBag.LanguageId = new SelectList(user.StudyingLanguages.ToList(), "Id", "Name", string.Empty);

            return PartialView("_EditWordPartial", word);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Word model)
        {
            if (!ModelState.IsValid || model == null)
            {
                TempData["error"] = InvalidWordDataMsg;
                return View(model);
            }

            var word = this.data.Words.Find(model.Id);

            for (int i = 0; i < word.Translations.Count; i++)
                word.Translations[i].Meaning = model.Translations[i].Meaning;

            for (int i = 0; i < word.Attributes.Count; i++)
                word.Attributes[i].Value = model.Attributes[i].Value;

            word.Name = model.Name;
            word.LastPracticed = DateTime.Now;

            this.data.SaveChanges();

            TempData["success"] = WordUpdatedMsg;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var word = this.data.Words.Find(id);

            if (word == null)
            {
                TempData["error"] = "Word not found";
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }

            this.data.Words.Delete(word);
            this.data.SaveChanges();

            TempData["success"] = "Word deleted";
            return View("Edit");
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
            this.GetTopWordsAndCacheIfNotCached();
            var topWords = this.HttpContext.Cache["topwords"];
            return PartialView("_TopWordsPartial", topWords);
        }

        [HttpGet]
        [Authorize]
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
                .Where(a => a.Value != null)
                .Project().To<WordAttribute>().ToList();

            word.Translations = translations
                .Where(t => t.Meaning != null)
                .Select(t => new WordTranslation { Language = language, Meaning = t.Meaning })
                .ToList();

            return word;
        }

        protected void ClearMeaningErrors(Word word)
        {
            if (word.Translations.Count == 0)
            {
                return;
            }

            ModelState.Keys
                .Where(k => k.Contains("Meaning"))
                .ForEach(k => ModelState[k].Errors.Clear());

            if (word.Attributes.Count > 0 ||
                word.PartOfSpeech == PartOfSpeech.Adjective)
            {
                ModelState.Keys
                    .Where(k => k.Contains("Value") || k.Contains("Name"))
                    .ForEach(k => ModelState[k].Errors.Clear());
            }
        }

        protected void GetTopWordsAndCacheIfNotCached()
        {
            if (this.HttpContext.Cache["topwords"] != null)
            {
                return;
            }

            var currLang = this.Session["language"].ToString().ToLower();

            var words = this.data.Words.All()
                .Where(w => w.Language.Name.ToLower() == currLang)
                .OrderBy(r => Guid.NewGuid())
                .Project()
                .To<WordShortViewModel>()
                .Take(6)
                .ToList();

            this.HttpContext.Cache.Add("topwords", words, null,
                DateTime.Now.AddHours(24), TimeSpan.Zero, CacheItemPriority.Default, null);
        }

        protected void ClearTranslationErrors()
        {
            var keys = ModelState.Keys.Where(k => k.Contains("Translation"));

            keys.ForEach(k => ModelState[k].Errors.Clear());
        }

        private string GetCurrentLanguage()
        {
            var langName = Session["language"].ToString().ToLower();
            return langName;
        }
    }
}