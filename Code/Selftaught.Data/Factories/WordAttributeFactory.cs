namespace Selftaught.Data.Factories
{
    using System;
    using System.Collections.Generic;

    using Selftaught.Data.Models;
    using Selftaught.Common.Extensions;

    public class WordAttributeFactory : IWordAttributeFactory
    {
        public ICollection<WordAttribute> GetAttributes(string language, string partOfSpeech)
        {
            language = language.ToLower();
            var partAsEnum = partOfSpeech.ToEnum<PartOfSpeech>();

            switch (language)
            {
                case "german":
                    return this.GetGermanAttributes(partAsEnum);
                case "spanish":
                    return this.GetSpanishAttributes(partAsEnum);

                default:
                    throw new ArgumentOutOfRangeException("Language not recognized.");
            }
        }

        protected virtual ICollection<WordAttribute> GetGermanAttributes(PartOfSpeech partOfSpeech)
        {
            switch (partOfSpeech)
            {
                case PartOfSpeech.Noun:
                    return this.GetGermanNounAttributes();
                case PartOfSpeech.Verb:
                    return this.GetGermanVerbAttributes();
                case PartOfSpeech.Adjective:
                    return this.GetGermanAdjectiveAttributes();
                case PartOfSpeech.Determiner:
                    return this.GetGermanAdjectiveAttributes();
                case PartOfSpeech.Pronoun:
                    return this.GetGermanAdjectiveAttributes();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual ICollection<WordAttribute> GetGermanNounAttributes()
        {
            var attrs = new SortedSet<WordAttribute>
            {
                new WordAttribute { Rank = 1, Name = "Article" },
                new WordAttribute { Rank = 2, Name = "Plural" },
            };

            return attrs;
        }

        protected virtual ICollection<WordAttribute> GetGermanVerbAttributes()
        {
            var attrs = new SortedSet<WordAttribute>
            {
                new WordAttribute { Rank = 1, Name = "Third person singular" },
                new WordAttribute { Rank = 2, Name = "Praterium" },
                new WordAttribute { Rank = 3, Name = "Partizip II" },
                new WordAttribute { Rank = 4, Name = "Prepositions" },
            };

            return attrs;
        }

        protected virtual ICollection<WordAttribute> GetGermanAdjectiveAttributes()
        {
            var attrs = new SortedSet<WordAttribute>();

            return attrs;
        }

        protected virtual ICollection<WordAttribute> GetSpanishAttributes(PartOfSpeech partOfSpeech)
        {
            var attrs = new SortedSet<WordAttribute>
            {
                new WordAttribute { Rank = 1, Name = "Fake" },
                new WordAttribute { Rank = 2, Name = "Test" },
                new WordAttribute { Rank = 3, Name = "Whatever" },
            };

            return attrs;
        }
    }
}
