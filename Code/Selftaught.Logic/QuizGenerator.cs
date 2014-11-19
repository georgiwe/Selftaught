namespace Selftaught.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Selftaught.Data.Models;
    using Selftaught.Logic.Contracts;
    using Selftaught.Common.Enumerations;

    public class QuizGenerator : IQuizGenerator
    {
        public virtual Quiz GenerateQuiz(Language lang, IEnumerable<Word> words, QuizType type)
        {
            switch (type)
            {
                case QuizType.ForeignToNative:
                    return this.GenerateForeignToNative(lang, words);
                case QuizType.NativeToForeign:
                    return this.GenerateNativeToForeign(lang, words);
                case QuizType.VerbPreposition:
                case QuizType.VerbTenses:
                default:
                    throw new ArgumentException();
            }
        }

        protected virtual Quiz GenerateNativeToForeign(Language lang, IEnumerable<Word> words)
        {
            var questions = this.GenerateQuestions(words, 
                w => w.Name,
                w => w.Translations.FirstOrDefault().Meaning);

            var quiz = new Quiz { Questions = questions };
            return quiz;
        }

        protected virtual Quiz GenerateForeignToNative(Language lang, IEnumerable<Word> words)
        {
            var questions = this.GenerateQuestions(words, 
                w => w.Translations.FirstOrDefault().Meaning,
                w => w.Name);

            var quiz = new Quiz { Questions = questions };
            return quiz;
        }

        protected virtual IList<QuizQuestion> GenerateQuestions(IEnumerable<Word> words, Func<Word, string> selectQuestion, Func<Word, string> selectAnswer)
        {
            var questions = new List<QuizQuestion>();

            foreach (var word in words)
            {
                var firstMeaning = word.Translations.FirstOrDefault().Meaning;

                var newQuestion = new QuizQuestion
                {
                    Question = selectQuestion(word),
                    Answer = selectAnswer(word),
                };

                questions.Add(newQuestion);
            }

            return questions;
        }
    }
}
