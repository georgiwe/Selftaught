using Selftaught.Web.Infrastructure.ModelMapping;
namespace Selftaught.Web.ViewModels.Language
{
    public class LanguageViewModel : IMapFrom<Selftaught.Data.Models.Language>
    {
        public string Name { get; set; }
    }
}