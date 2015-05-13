using System.Windows.Threading;
using ITorshifyPlaylist = Spotify.Client.Infrastructure.Interfaces.IPlaylist;

using ITorshifyPlaylistTrack = Spotify.Client.Infrastructure.Interfaces.IPlaylistTrack;

namespace Spotify.Client.Spotify.Services
{
    public class PlaylistTrack : Track, ITorshifyPlaylistTrack
    {
        #region Constructors

        public PlaylistTrack(ITorshifyPlaylist parentPlaylist, IPlaylistTrack track, Dispatcher dispatcher)
            : base(track, dispatcher)
        {
            Playlist = parentPlaylist;
        }

        #endregion Constructors

        #region Properties

        public ITorshifyPlaylist Playlist
        {
            get;
            private set;
        }

        #endregion Properties
    }
}