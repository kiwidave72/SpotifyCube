using System;
using System.Linq;
using System.Windows.Threading;

using Microsoft.Practices.Prism.ViewModel;

using Spotify.Client.Infrastructure.Collections;
using Spotify.Client.Infrastructure.Interfaces;

using ITorshifyImage = Spotify.Client.Infrastructure.Interfaces.IImage;

using ITorshifyAlbum = Spotify.Client.Infrastructure.Interfaces.IAlbum;

using ITorshifyArtist = Spotify.Client.Infrastructure.Interfaces.IArtist;

using ITorshifyTrack = Spotify.Client.Infrastructure.Interfaces.ITrack;

namespace Spotify.Client.Spotify.Services
{
    public class ArtistInformation : NotificationObject, IArtistInformation
    {
        #region Fields

        private readonly Artist _artist;
        private readonly Dispatcher _dispatcher;

        private NotifyCollection<Album> _albums;
        private string _biography;
        private IArtistBrowse _browse;
        private bool _isLoading;
        private NotifyCollection<Image> _portraits;
        private NotifyCollection<Artist> _similarArtists;
        private NotifyCollection<Track> _tracks;
        private ITorshifyImage _firstPortait;

        #endregion Fields

        #region Constructors

        public ArtistInformation(Artist artist, Dispatcher dispatcher)
        {
            _portraits = new NotifyCollection<Image>();
            _tracks = new NotifyCollection<Track>();
            _albums = new NotifyCollection<Album>();
            _similarArtists = new NotifyCollection<Artist>();
            _dispatcher = dispatcher;
            _artist = artist;
            _browse = _artist.InternalArtist.Browse();
            _isLoading = !_browse.IsComplete;
            _browse.Completed += ArtistBrowseCompleted;
        }

        #endregion Constructors

        #region Events

        public event EventHandler FinishedLoading;

        #endregion Events

        #region Properties

        public INotifyEnumerable<ITorshifyAlbum> Albums
        {
            get
            {
                return _albums;
            }
        }

        public string Biography
        {
            get
            {
                return _biography;
            }
            private set
            {
                if (_biography != value)
                {
                    _biography = value;
                    RaisePropertyChanged("Biography");
                }
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            private set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public ITorshifyImage FirstPortrait
        {
            get { return _firstPortait; }
            private set
            {
                _firstPortait = value;
                RaisePropertyChanged("FirstPortait");
            }
        }

        public INotifyEnumerable<ITorshifyImage> Portraits
        {
            get
            {
                return _portraits;
            }
        }

        public INotifyEnumerable<ITorshifyArtist> SimilarArtists
        {
            get
            {
                return _similarArtists;
            }
        }

        public INotifyEnumerable<ITorshifyTrack> Tracks
        {
            get
            {
                return _tracks;
            }
        }

        #endregion Properties

        #region Methods

        private void ArtistBrowseCompleted(object sender, EventArgs e)
        {
            IArtistBrowse browse = (IArtistBrowse)sender;
            browse.Completed -= ArtistBrowseCompleted;
            _dispatcher.BeginInvoke(new Action<IArtistBrowse>(LoadBrowseData), DispatcherPriority.Background, browse);
        }

        private void LoadBrowseData(IArtistBrowse browse)
        {
            using (browse)
            {
                Biography = browse.Biography;

                foreach (var spotifyAlbum in browse.Albums)
                {
                    _albums.Add(new Album(_artist, spotifyAlbum, _dispatcher));
                }

                foreach (var spotifyTrack in browse.Tracks)
                {
                    _tracks.Add(new Track(spotifyTrack, _dispatcher));
                }

                foreach (var spotifyArtist in browse.SimilarArtists)
                {
                    _similarArtists.Add(new Artist(spotifyArtist, _dispatcher));
                }

                foreach (var spotifyPortrait in browse.Portraits)
                {
                    using (spotifyPortrait)
                    {
                        _portraits.Add(new Image(_artist.InternalArtist.Session, spotifyPortrait.ImageId));
                    }
                }

                FirstPortrait = _portraits.FirstOrDefault();
            }

            IsLoading = false;
            RaiseFinishedLoading();
        }

        private void RaiseFinishedLoading()
        {
            var handler = FinishedLoading;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion Methods
    }
}