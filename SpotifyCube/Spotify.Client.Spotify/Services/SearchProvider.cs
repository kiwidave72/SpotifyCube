using System.Windows.Threading;

using Spotify.Client.Infrastructure.Interfaces;

using ITorshifySearch = Spotify.Client.Infrastructure.Interfaces.ISearch;
using Torshify;

namespace Spotify.Client.Spotify.Services
{
    public class SearchProvider : ISearchProvider
    {
        #region Fields

        private readonly Dispatcher _dispatcher;
        private readonly ISession _session;

        #endregion Fields

        #region Constructors

        public SearchProvider(ISession session, Dispatcher dispatcher)
        {
            _session = session;
            _dispatcher = dispatcher;
        }

        #endregion Constructors

        #region Methods

        public ITorshifySearch Search(string query, int trackOffset, int trackCount, int albumOffset, int albumCount, int artistOffset, int artistCount, object userData = null)
        {
            var spotifySearch = _session.Search(
                query,
                trackOffset,
                trackCount,
                albumOffset,
                albumCount,
                artistOffset,
                artistCount,
                0,
                0,
                SearchType.Standard,
                userData);

            return new Search(spotifySearch, _dispatcher);
        }

        #endregion Methods
    }
}