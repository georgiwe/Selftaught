namespace Selftaught.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Selftaught.Data.Common.ModelAdditions;

    public class WordTranslation : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        // TODO: required
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        [Required]
        public string Meaning { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
