namespace Selftaught.Logic.Contracts
{
    using System.Collections.Generic;

    using Selftaught.Common.Enumerations;
    using Selftaught.Data.Models;

    public interface IQuizGenerator
    {
        Quiz GenerateQuiz(Language lang, IEnumerable<Word> words, QuizType type);
    }
}
