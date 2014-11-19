namespace Selftaught.Web.ViewModels.Quizzes
{
    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class QuizQuestionViewModel : IMapFrom<QuizQuestion>
    {
        //[HiddenInput(DisplayValue = false)]
        //public int CurrentQuestion { get; set; }

        [Display(Name = "Question:")]
        public string Question { get; set; }

        [Required]
        [UIHint("TextBox")]
        [Display(Name = "Your Answer:")]
        public string Answer { get; set; }

        public string UserAnswer { get; set; }

        public bool IsCorrect { get; set; }
    }
}