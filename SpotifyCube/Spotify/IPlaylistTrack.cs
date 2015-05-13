using System;

namespace Spotify
{
    public interface IPlaylistTrack : ITrack
    {
        DateTime CreateTime
        {
            get;
        }

        bool Seen
        {
            get;
        }

        IPlaylist Playlist
        {
            get;
        }
    }
}
