namespace Selftaught.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Selftaught.Data.Models;
    using Selftaught.Web.ViewModels.Words;
    
    public class HomePageDataViewModel
    {
        public IEnumerable<WordDetailedViewModel> TopWords { get; set; }

        public IEnumerable<ApplicationUser> LatestUsers { get; set; }
    }
}