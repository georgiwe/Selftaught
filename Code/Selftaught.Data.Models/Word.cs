namespace Selftaught.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Selftaught.Data.Common.ModelAdditions;

    public class Word : AuditInfo, IDeletableEntity
    {
        private IList<WordTranslation> translations;
        private IList<WordAttribute> attributes;

        public Word()
        {
            this.translations = new List<WordTranslation>();
            this.attributes = new List<WordAttribute>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IList<WordAttribute> Attributes
        {
            get { return this.attributes; }

            set { this.attributes = value; }
        }

        [Required]
        public virtual IList<WordTranslation> Translations
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
