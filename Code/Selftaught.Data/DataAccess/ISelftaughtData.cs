
namespace Selftaught.Data.DataAccess
{
    using Selftaught.Data.Common.Repositories;
    using Selftaught.Data.Models;

    public interface ISelftaughtData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Word> Words { get; }

        IRepository<Language> Languages { get; }

        IRepository<WordTranslation> Translations { get; }

        IRepository<WordAttribute> WordAttributes { get; }

        int SaveChanges();
    }
}
