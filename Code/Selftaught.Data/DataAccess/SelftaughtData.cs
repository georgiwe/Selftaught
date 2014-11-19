using Selftaught.Data.Common.Repositories;
using Selftaught.Data.Models;
using System.Data.Entity;
namespace Selftaught.Data.DataAccess
{
    public class SelftaughtData : ISelftaughtData
    {
        private DbContext context;
        private IRepository<ApplicationUser> users;
        private IRepository<Word> words;
        private IRepository<Language> languages;
        private IRepository<WordTranslation> translations;
        private IRepository<WordAttribute> wordAttributes;
        private IRepository<Quiz> quizzes;
        private IRepository<QuizQuestion> quizQuestions;

        public SelftaughtData(DbContext context)
        {
            this.context = context;
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new Repository<ApplicationUser>(this.context);
                }

                return this.users;
            }
        }

        public IRepository<Word> Words
        {
            get
            {
                if (this.words == null)
                {
                    this.words = new DeletableEntityRepository<Word>(this.context);
                }

                return this.words;
            }
        }

        public IRepository<Language> Languages
        {
            get
            {
                if (this.languages == null)
                {
                    this.languages = new DeletableEntityRepository<Language>(this.context);
                }

                return this.languages;
            }
        }

        public IRepository<WordTranslation> Translations
        {
            get
            {
                if (this.translations == null)
                {
                    this.translations = new DeletableEntityRepository<WordTranslation>(this.context);
                }

                return this.translations;
            }
        }

        public IRepository<WordAttribute> WordAttributes
        {
            get
            {
                if (this.wordAttributes == null)
                {
                    this.wordAttributes = new DeletableEntityRepository<WordAttribute>(this.context);
                }

                return this.wordAttributes;
            }
        }

        public IRepository<Quiz> Quizzes
        {
            get
            {
                if (this.quizzes == null)
                {
                    this.quizzes = new DeletableEntityRepository<Quiz>(this.context);
                }

                return quizzes;
            }
        }

        public IRepository<QuizQuestion> QuizQuestions
        {
            get
            {
                if (this.quizQuestions == null)
                {
                    this.quizQuestions = new DeletableEntityRepository<QuizQuestion>(this.context);
                }

                return quizQuestions;
            }
        }

        public DbContext Context
        {
            get { return this.context; }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
