namespace Selftaught.Data.DataAccess
{
    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.Models;

    public interface IUsersLanguagesData
    {
        IRepository<ApplicationUser> Users { get; set; }

        IRepository<Language> Languages { get; set; }
    }
}
