using System;

namespace Spotify
{
    public interface ITrackAndOffset
    {
        ITrack Track
        {
            get;
        }

        TimeSpan Offset
        {
            get;
        }
    }
}
