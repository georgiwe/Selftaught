using Selftaught.Data.Models;
using System.Collections.Generic;
namespace Selftaught.Web.Models
{
    public class WordViewModel
    {
        public string Name { get; set; }

        public ICollection<WordTranslation> Translations { get; set; }

        public PartOfSpeech PartOfSpeech { get; set; }
    }
}