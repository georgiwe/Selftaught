namespace Selftaught.Web.ViewModels.Words
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;


    public class WordShortViewModel : IRequiresCustomMapping
    {
        public string Name { get; set; }

        public IEnumerable<string> Translations { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<Word, WordShortViewModel>()
                .ForMember(dest => dest.Translations,
                           opts => opts.MapFrom(src => src.Translations.Select(t => t.Meaning)));
            configuration.CreateMap<WordShortViewModel, Word>()
                .ForMember(dest => dest.Translations,
                           opts => opts.MapFrom(src => src.Translations.Select(t => new WordTranslation { Meaning = t })));
        }
    }
}