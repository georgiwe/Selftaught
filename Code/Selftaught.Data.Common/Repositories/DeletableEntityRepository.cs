namespace Selftaught.Data.Common.Repositories
{
    using System.Linq;
    using System.Data.Entity;

    using Selftaught.Data.Common.ModelAdditions;

    public class DeletableEntityRepository<T> : Repository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(e => e.IsDeleted == false);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }
    }
}
