namespace Selftaught.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Selftaught.Data.Common.ModelAdditions;

    public class WordAttribute : AuditInfo, IDeletableEntity, IComparable<WordAttribute>
    {
        [Key]
        public int Id { get; set; }

        public int Rank { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CompareTo(WordAttribute other)
        {
            return this.Rank.CompareTo(other.Rank);
        }
    }
}
