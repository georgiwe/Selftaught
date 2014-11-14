namespace Selftaught.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Selftaught.Data.Common.ModelAdditions;

    public class Word : AuditInfo, IDeletableEntity
    {
        private ICollection<WordTranslation> translations;
        private ICollection<WordAttribute> attributes;

        public Word()
        {
            this.translations = new HashSet<WordTranslation>();
            this.attributes = new HashSet<WordAttribute>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<WordAttribute> Attributes
        {
            get { return this.attributes; }

            set { this.attributes = value; }
        }

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
