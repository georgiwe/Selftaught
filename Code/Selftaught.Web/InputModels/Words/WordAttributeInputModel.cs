namespace Selftaught.Web.InputModels.Words
{
    using System.ComponentModel.DataAnnotations;

    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;
    
    public class WordAttributeInputModel : IMapFrom<WordAttribute>
    {
        [Required]
        public int Rank { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}