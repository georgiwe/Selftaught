namespace Selftaught.Data.Common.ModelAdditions
{
    using System;

    public interface IDeletableEntry
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
