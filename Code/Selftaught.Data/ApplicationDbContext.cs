namespace Selftaught.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Selftaught.Data.Models;
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
