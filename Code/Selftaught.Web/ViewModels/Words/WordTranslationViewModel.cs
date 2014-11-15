namespace Selftaught.Web.ViewModels.Words
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;
    using System.Collections.Generic;

    public class WordTranslationViewModel : IRequiresCustomMapping
    {
        public string Language { get; set; }

        public string Meaning { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<WordTranslation, WordTranslationViewModel>()
                .ForMember(dest => dest.Language,
                           opts => opts.MapFrom(src => src.Language.Name))
                .ForMember(dest => dest.Meaning,
                           opts => opts.MapFrom(src => src.Meaning));
        }
    }
}
