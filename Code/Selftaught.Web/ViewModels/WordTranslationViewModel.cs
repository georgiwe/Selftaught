namespace Selftaught.Data.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Selftaught.Web.Infrastructure.ModelMapping;

    public class WordTranslationViewModel : IRequiresCustomMapping
    {
        [Required]
        public string Language { get; set; }

        [Required]
        public string Meaning { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<WordTranslation, WordTranslationViewModel>()
                .ForMember(dest => dest.Language,
                           opts => opts.MapFrom(src => src.Language.Name));
        }
    }
}
