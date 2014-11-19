namespace Selftaught.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using AutoMapper.QueryableExtensions;

    using Selftaught.Common.Constants;
    using Selftaught.Data.DataAccess;
    using Selftaught.Common.Enumerations;
    using Selftaught.Common.Extensions;
    using Selftaught.Logic.Contracts;
    using Selftaught.Web.ViewModels.Quizzes;
    using Selftaught.Web.Infrastructure.Helpers;
    using Selftaught.Data.Models;

    [Authorize]
    public class QuizzesController : BaseController
    {
        private const string Language = "language";
        private const int DefaultQuestionCount = 10;

        public QuizzesController(ISelftaughtData data, IQuizGenerator quizGen)
            : base(data)
        {
            this.QuizGen = quizGen;
        }

        protected IQuizGenerator QuizGen { get; set; }

        // GET: Quizes
        public ActionResult Index()
        {
            var quizNames = Enum.GetNames(typeof(QuizType))
                .Select(n => new SelectListItem
                {
                    Value = n,
                    Text = n
                });
            return View(quizNames);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult GetQuiz(string quizType, bool onlyMine)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }

            var langName = Session[Language].ToString().ToLower();
            var lang = this.data.Languages.All()
                .FirstOrDefault(l => l.Name.ToLower() == langName);

            var userId = this.User.Identity.GetUserId();
            var wordsQuery = this.data.Words.All();

            if (onlyMine)
            {
                wordsQuery = wordsQuery.Where(w => w.AddedByUserId == userId);
            }

            wordsQuery = wordsQuery
                .Where(w => w.LanguageId == lang.Id)
                .OrderBy(w => Guid.NewGuid())
                .Take(DefaultQuestionCount);

            var words = wordsQuery.ToList();

            var quizTypeEnum = quizType.ToEnum<QuizType>();

            var quiz = this.QuizGen.GenerateQuiz(lang, words, quizTypeEnum);

            if (quiz.Questions.Count == 0)
            {
                TempData["error"] = "You have not added any words in " + Session[Language];
                return RedirectToAction("Index");
            }

            this.data.Quizzes.Add(quiz);
            this.data.SaveChanges();

            var progressHelper = new QuizProgressHelper { Quiz = quiz, CurrentQuestionIndex = 0 };
            Session[Const.Quiz] = progressHelper;

            return View("Question", quiz.Questions.First());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GiveAnswer(QuizQuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Answer is required.";
            }

            // TODO: Rename to quiz
            var progress = Session[Const.Quiz] as QuizProgressHelper;

            if (progress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }

            var answeredQuestion = 
                progress.Quiz.Questions[progress.CurrentQuestionIndex];

            answeredQuestion.UserAnswer = model.Answer;

            this.data.QuizQuestions.Update(answeredQuestion);
            this.data.SaveChanges();

            if (progress.CurrentQuestionIndex < DefaultQuestionCount - 1)
            {
                progress.CurrentQuestionIndex++;

                var nextQuestion = progress.Quiz.Questions[progress.CurrentQuestionIndex];
                return View("Question", nextQuestion);
            }

            var quizData = progress.Quiz.Questions
                .AsQueryable()
                .Project()
                .To<QuizQuestionViewModel>()
                .ToArray()
                .ForEach(q =>
                    q.IsCorrect = q.Answer.ToLower() == q.UserAnswer.ToLower())
                .ToList();

            return View("QuizFinished", quizData);
        }
    }
}