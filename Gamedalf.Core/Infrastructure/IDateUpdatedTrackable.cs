using System;

namespace Gamedalf.Core.Infrastructure
{
    public interface IDateUpdatedTrackable
    {
        DateTime? DateUpdated { get; set; }
    }
}
