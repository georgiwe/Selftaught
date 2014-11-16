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
<<<<<<< HEAD
        public int Id { get; set; }
=======
        public WordDetailedViewModel()
        {
        }
>>>>>>> 80587a54c5a2f5d966c43f2895b73cb028661095

        [Required]
        public string Name { get; set; }

        [Required]
        public IList<WordTranslationViewModel> Translations { get; set; }

        [Required]
        public IList<WordAttributeViewModel> Attributes { get; set; }

        [Required]
        public PartOfSpeech PartOfSpeech { get; set; }

<<<<<<< HEAD
        [Required]
=======
>>>>>>> 80587a54c5a2f5d966c43f2895b73cb028661095
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
