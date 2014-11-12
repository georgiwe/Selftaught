namespace Selftaught.Web.InputModels.Words
{
    using Selftaught.Data.Models;
    using System.Collections.Generic;

    public class WordInputModel
    {
        public string Name { get; set; }

        public ICollection<WordTranslation> Translations { get; set; }

        public PartOfSpeech PartOfSpeech { get; set; }
    }
}