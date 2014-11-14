namespace Selftaught.Web.ViewModels.Words
{
    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;

    public class WordAttributeViewModel : IMapFrom<WordAttribute>
    {
        public int Rank { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}