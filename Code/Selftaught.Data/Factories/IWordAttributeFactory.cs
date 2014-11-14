namespace Selftaught.Data.Factories
{
    using System.Collections.Generic;
    using Selftaught.Data.Models;

    public interface IWordAttributeFactory
    {
        ICollection<WordAttribute> GetAttributes(string language, string partOfSpeech);
    }
}
