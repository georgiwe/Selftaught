namespace Selftaught.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Selftaught.Common.Enumerations;
    using Selftaught.Data.Common.ModelAdditions;

    public class Quiz : AuditInfo, IDeletableEntity
    {
        private IList<QuizQuestion> questions;

        public Quiz()
        {
            this.questions = new List<QuizQuestion>();
        }

        [Key]
        public int Id { get; set; }

        public virtual IList<QuizQuestion> Questions
        {
            get
            {
                return this.questions;
            }

            set
            {
                this.questions = value;
            }
        }

        public QuizType Type { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
