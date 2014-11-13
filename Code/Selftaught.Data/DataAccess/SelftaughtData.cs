using Selftaught.Data.Common.Repositories;
using Selftaught.Data.Models;
using System.Data.Entity;
namespace Selftaught.Data.DataAccess
{
    public class SelftaughtData : ISelftaughtData
    {
        private DbContext context;
        private Repository<ApplicationUser> users;
        private Repository<Word> words;
        private Repository<Language> languages;

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
    }
}
