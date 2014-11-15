namespace Selftaught.Web.ViewModels.Words
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;
    using Selftaught.Web.ViewModels.Language;

    public class WordDetailedViewModel : IMapFrom<Word>, IRequiresCustomMapping
    {
        public WordDetailedViewModel()
        {
        }

        public string Name { get; set; }

        public ICollection<WordTranslationViewModel> Translations { get; set; }

        public ICollection<WordAttributeViewModel> Attributes { get; set; }

        public PartOfSpeech PartOfSpeech { get; set; }

        public string Language { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<Word, WordDetailedViewModel>()
                .ForMember(d => d.Language,
                           o => o.MapFrom(s => s.Language.Name));
        }
    }
}
