namespace Selftaught.Data.DataAccess
{
    using System;
    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.Models;
    using System.Data.Entity;

    public class UsersLanguagesData : IUsersLanguagesData
    {
        private DbContext context;
        private IRepository<Language> languages;
        private IRepository<ApplicationUser> users;

        public UsersLanguagesData(DbContext context)
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
            set
            {
                this.users = value;
            }
        }

        public IRepository<Language> Languages
        {
            get
            {
                if (this.languages == null)
                {
                    this.languages = new Repository<Language>(this.context);
                }

                return this.languages;
            }
            set
            {
                this.languages = value;
            }
        }
    }
}
