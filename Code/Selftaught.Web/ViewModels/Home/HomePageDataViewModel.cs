namespace Selftaught.Web.ViewModels.Home
{
    using Selftaught.Web.ViewModels.Words;
    using System.Collections.Generic;
    
    public class HomePageDataViewModel
    {
        public IEnumerable<WordDetailedViewModel> TopWords { get; set; }

    }
}