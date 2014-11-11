namespace Selftaught.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Selftaught.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            // TODO: switch to false
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Words.Any())
            {
                return;
            }

            var rnd = new Random();
            var german = new Language() { Name = "German" };
            var user = new ApplicationUser()
            {
                UserName = "wah@wah.wah",
                Email = "wah@wah.wah"
            };

            for (int i = 0; i < 100; i++)
            {
                var newWord = new Word();
                newWord.AddedByUser = user;
                newWord.Language = german;
                newWord.LastPracticed = DateTime.Now;
                newWord.Name = string.Format("randomword{0}", i + 1);
                newWord.PartOfSpeech = PartOfSpeech.Noun;
                newWord.Translations = new List<WordTranslation>()
                    {
                        new WordTranslation() 
                        { 
                            Language = german, 
                            Meaning = string.Format("meaning{0}", i + 1),
                        }
                    };

                if (i % 4 == 0)
                {
                    var transl = new WordTranslation
                    {
                        Language = german,
                        Meaning = string.Format("second meaning{0}", i + 1)
                    };

                    newWord.Translations.Add(transl);
                }

                context.Words.Add(newWord);
            }

            context.SaveChanges();
        }
    }
}
