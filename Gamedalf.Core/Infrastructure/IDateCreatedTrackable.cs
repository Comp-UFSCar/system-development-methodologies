using System;

namespace Gamedalf.Core.Infrastructure
{
    /// <summary>
    /// Define attribute DateCreated
    /// </summary>
    public interface IDateCreatedTrackable
    {
        DateTime DateCreated { get; set; }
    }
}
