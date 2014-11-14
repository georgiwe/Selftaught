namespace Selftaught.Web.ViewModels.Words
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;

    public class WordDetailedViewModel : IMapFrom<Word>
    {
        public WordDetailedViewModel()
        {
            this.Translations = new HashSet<WordTranslationViewModel>();
        }

        public string Name { get; set; }

        public ICollection<WordTranslationViewModel> Translations { get; set; }

        public ICollection<WordAttributeViewModel> Attributes { get; set; }

        public PartOfSpeech PartOfSpeech { get; set; }

        public DateTime? LastPracticed { get; set; }
    }
}
