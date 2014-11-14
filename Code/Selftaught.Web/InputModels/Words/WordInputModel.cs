namespace Selftaught.Web.InputModels.Words
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Selftaught.Data.Models;
    using Selftaught.Web.Infrastructure.ModelMapping;
    using Selftaught.Web.ViewModels.Words;

    public class WordInputModel : IMapFrom<Word>
    {
        public WordInputModel()
        {
            this.Translations = new HashSet<WordTranslationViewModel>();
        }

        [Display(Name = "Word:")]
        [Required]
        public string Name { get; set; }

        [Required]
        [UIHint("TranslationsEditorTemplate")]
        // TODO: custom validation attribute
        public ICollection<WordTranslationViewModel> Translations { get; set; }

        [Required]
        public PartOfSpeech PartOfSpeech { get; set; }
    }
}