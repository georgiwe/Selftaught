namespace Selftaught.Data.Models
{
    using Selftaught.Data.Common.ModelAdditions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Word : AuditInfo, IDeletableEntity
    {
        private ICollection<WordTranslation> translations;

        public Word()
        {
            this.translations = new HashSet<WordTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual ICollection<WordTranslation> Translations
        {
            get { return this.translations; }

            set { this.translations = value; }
        }

        [Required]
        public PartOfSpeech PartOfSpeech { get; set; }

        public DateTime? LastPracticed { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
