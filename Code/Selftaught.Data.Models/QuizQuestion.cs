namespace Selftaught.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Selftaught.Data.Common.ModelAdditions;

    public class QuizQuestion : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        public string Answer { get; set; }

        public string UserAnswer { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz FromQuiz { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
