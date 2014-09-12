
namespace Gamedalf.Core.Infrastructure
{
    /// <summary>
    /// Keep track of entities timestamps.
    /// This interface should be implemented by an DbContext implementation.
    /// </summary>
    public interface IDateTracker
    {
        void TrackDate();
    }
}
