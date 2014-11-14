using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Selftaught.Data;
using Selftaught.Data.Models;
using Selftaught.Data.DataAccess;
using Selftaught.Data.Factories;

namespace Selftaught.Web.Controllers
{
    public class WordsScaffoldedController : BaseController
    {
        private IWordAttributeFactory wordAttrsFact;

        public WordsScaffoldedController(ISelftaughtData data, IWordAttributeFactory wordAttrsFact)
            : base(data)
        {
            this.wordAttrsFact = wordAttrsFact;
        }

        // GET: WordsScaffolded
        public ActionResult Index()
        {
            var words = this.data.Words.All().Include(w => w.AddedByUser).Include(w => w.Language);
            return View(words.ToList());
        }

        // GET: WordsScaffolded/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = this.data.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // GET: WordsScaffolded/Create
        public ActionResult Create()
        {
            ViewBag.AddedByUserId = new SelectList(this.data.Users.All(), "Id", "Email");
            ViewBag.LanguageId = new SelectList(this.data.Languages.All(), "Id", "Name");
            return View();
        }

        // POST: WordsScaffolded/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PartOfSpeech,LastPracticed,AddedByUserId,LanguageId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Word word)
        {
            if (ModelState.IsValid)
            {
                this.data.Words.Add(word);
                this.data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddedByUserId = new SelectList(this.data.Users.All(), "Id", "Email", word.AddedByUserId);
            ViewBag.LanguageId = new SelectList(this.data.Languages.All(), "Id", "Name", word.LanguageId);
            return View(word);
        }

        // GET: WordsScaffolded/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = this.data.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddedByUserId = new SelectList(this.data.Users.All(), "Id", "Email", word.AddedByUserId);
            ViewBag.LanguageId = new SelectList(this.data.Languages.All(), "Id", "Name", word.LanguageId);
            return View(word);
        }

        // POST: WordsScaffolded/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PartOfSpeech,LastPracticed,AddedByUserId,LanguageId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Word word)
        {
            if (ModelState.IsValid)
            {
                this.data.Words.Update(word);
                this.data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddedByUserId = new SelectList(this.data.Users.All(), "Id", "Email", word.AddedByUserId);
            ViewBag.LanguageId = new SelectList(this.data.Languages.All(), "Id", "Name", word.LanguageId);
            return View(word);
        }

        // GET: WordsScaffolded/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = this.data.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: WordsScaffolded/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Word word = this.data.Words.Find(id);
            this.data.Words.Delete(word);
            this.data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
