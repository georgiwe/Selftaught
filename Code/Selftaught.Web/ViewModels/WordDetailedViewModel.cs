namespace Selftaught.Data.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Selftaught.Web.Infrastructure.ModelMapping;

    public class WordDetailedViewModel : IMapFrom<Word>
    {
        public WordDetailedViewModel()
        {
            this.Translations = new HashSet<WordTranslationViewModel>();
        }

        public string Name { get; set; }

        public ICollection<WordTranslationViewModel> Translations { get; set; }

        public PartOfSpeech PartOfSpeech { get; set; }

        public DateTime? LastPracticed { get; set; }
    }
}
