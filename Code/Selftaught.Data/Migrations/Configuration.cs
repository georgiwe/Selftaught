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

            var german = new Language { Name = "German" };
            var path = @"D:\Repositories\Selftaught\Code\Selftaught.Data\GermanWords\final.txt";
            var user = new ApplicationUser
            {
                UserName = "admin@admin.admin",
                PasswordHash = "AE8InMXLvYe6WLscZ2HY5k5oxGxmGmc9+ZmUQ/TM5u7hs2FtFxQERS4FI4Kh7YAxNA==",
                SecurityStamp = "63afc286-60e9-40fa-9d6c-54802ac16df3",
            };

            using (var sr = new System.IO.StreamReader(path))
            {
                var line = string.Empty;
                context.Configuration.AutoDetectChangesEnabled = false;

                while ((line = sr.ReadLine()) != null)
                {
                    var words = line.Split();
                    var eng = words[0];
                    var ger = words[1];

                    if (char.IsUpper(ger[0]) == false)
                    {
                        continue;
                    }

                    var wordToAdd = new Word
                    {
                        AddedByUser = null,
                        CreatedOn = DateTime.Now,
                        Language = german,
                        LastPracticed = DateTime.Now,
                        Name = eng,
                        PartOfSpeech = PartOfSpeech.Noun,
                        Translations = new List<WordTranslation>() { new WordTranslation { LanguageId = 1, Meaning = ger, } },
                    };

                    context.Words.Add(wordToAdd);
                }
            }

            var lang2 = new Language { Name = "Spanish" };
            var lang3 = new Language { Name = "Italian" };

            context.Languages.Add(lang2);
            context.Languages.Add(lang3);
            context.Users.Add(user);
            context.SaveChanges();
            context.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}
