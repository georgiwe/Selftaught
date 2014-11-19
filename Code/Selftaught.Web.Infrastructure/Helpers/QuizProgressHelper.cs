namespace Selftaught.Web.Infrastructure.Helpers
{
    using Selftaught.Data.Models;

    public class QuizProgressHelper
    {
        public Quiz Quiz { get; set; }

        public int CurrentQuestionIndex { get; set; }
    }
}
