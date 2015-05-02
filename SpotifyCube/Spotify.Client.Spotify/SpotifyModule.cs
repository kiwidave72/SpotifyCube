using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

using Spotify.Client.Infrastructure;
using Spotify.Client.Infrastructure.Interfaces;
using Spotify.Client.Spotify.Services;
using Spotify.Client.Spotify.Views.Login;
using Spotify.Client.Spotify.Views.Playlists;
using Torshify;

namespace Spotify.Client.Spotify
{
    public class SpotifyModule : IModule
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        #endregion Fields

        #region Constructors

        public SpotifyModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        #endregion Constructors

        #region Properties

        protected static ISession Session
        {
            get;
            private set;
        }

        #endregion Properties

        #region Public Static Methods

        public static void InitializeLibspotify()
        {
            Session = SessionFactory.CreateSession(
                Constants.ApplicationKey,
                Constants.CacheFolder,
                Constants.SettingsFolder,
                Constants.UserAgent);

            Application.Current.Exit += delegate
            {
                if (Session != null)
                {
                    try
                    {
                        Session.Dispose();
                    }
                    catch
                    {
                        
                    }
                }
            };
        }

        #endregion Public Static Methods

        #region Public Methods

        public void Initialize()
        {
            _container.RegisterInstance(Session);
            _container.RegisterType<IPlaylistProvider, PlaylistProvider>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPlayerController, PlayerController>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISearchProvider, SearchProvider>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPlayer, NAudioPlayer>(new ContainerControlledLifetimeManager());
            _container.RegisterType<LoginView>("LoginView");
            _container.RegisterType<PlaylistNavigationView>("SpotifyPlaylistNavigation");
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(LoginView));
            _regionManager.RegisterViewWithRegion("Navigation", typeof(PlaylistNavigationView));
            _container.Resolve<SpotifyLogging>().Initialize();
            _container.Resolve<SpotifyLinkNavigator>().Initialize();
        }

        #endregion Public Methods
    }
}