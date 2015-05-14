using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cube.Test;
using Spotify.Client.Spotify;


using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

using Spotify.Client.Modules.Core;

using Spotify.Client.Infrastructure;
using Spotify.Client.Infrastructure.Interfaces;
using Spotify.Client.Infrastructure.Services;
using Spotify.Cube.UI.Test.Log;

using Spotify.Client.Infrastructure.Models;

using Microsoft.Practices.Prism.Logging;
using Spotify.Client.Spotify.Services;

namespace Spotify.Cube.UI.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ISession Session;
        IPlayer Player;
        ILoggerFacade logging;

        PlayerController playerController;

        private MainWindowModelView view;

        private float DefaultVolume;
        public bool DefaultVolumeSet { get; set; }

        private System.Timers.Timer playsleeper;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            playsleeper = new System.Timers.Timer(2000);
            playsleeper.Elapsed += playsleeper_Elapsed;
            playsleeper.Enabled = false;

            InitializeLogging();

            view = new MainWindowModelView();

            if (view.IsInitialized == false)
            {
                return;
            }

            this.DataContext = view;

            Player = new NAudioPlayer();
            logging = new Log4NetFacade();
            new SpotifyLogging(Session, logging);


            Session = SessionFactory.CreateSession(
                    Constants.ApplicationKey,
                    Constants.CacheFolder,
                    Constants.SettingsFolder,
                    Constants.UserAgent);

            Session.LoginComplete += UserLoggedIn;
            Session.ConnectionError += ConnectionError;


            #region
            Session.Login("kiwidave72", "Yokomo08", false);
            #endregion



            view.model.GestureChanged += model_GestureChanged;

        }

        
      

        void model_GestureChanged(object sender, Library.CubeGestureEventArgs e)
        {
            if (CanChangePlayState  == true && e.Gesture == "Play/Stop" && playerController.IsPlaying==false)
            {
                playerController.Play();

                CanChangePlayState = false;
                playsleeper.Enabled = true;

                return;

            }
            else if (CanChangePlayState == true &&  e.Gesture == "Play/Stop" && playerController.IsPlaying)
            {
                playerController.Pause();

                playsleeper.Enabled = true;
                CanChangePlayState = false;

                return;
            }

            else if (e.Gesture == "Volume" && playerController != null)
            {

                view.Volume = (float) e.Value;
                playerController.Volume = (float) e.Value / 100 ;
                Console.WriteLine( string.Format("playerController.Volume -> {0}", playerController.Volume));

            }
            else if (e.Gesture == "Default Volume")
            {
                DefaultVolume = (float)e.Value;
                DefaultVolumeSet = true;
            }
        }

        void playsleeper_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            playsleeper.Enabled = false;
            
            CanChangePlayState = true;

        }

        private void UserLoggedIn(object sender, SessionEventArgs e)
        {
            if (e.Status == Error.OK)
            {
                Console.WriteLine("Successfully logged in", ConsoleColor.Yellow);

                var playListProvider = new PlaylistProvider(Session, Application.Current.Dispatcher, logging);

                playerController = new PlayerController(Session, Player, Application.Current.Dispatcher, logging, null);
                playerController.Volume = DefaultVolume /100;

                Session.PlaylistContainer.Loaded += PlaylistContainer_Loaded;

                //var provider = new SearchProvider(Session, Application.Current.Dispatcher);

                //var search = provider.Search("Pink Floyd", 0, 0, 0, 250, 0, 0);

                //search.FinishedLoading += OnSearchFinishedLoading;

            }
            else
            {
                Console.WriteLine("Unable to log in: " + e.Status.GetMessage(), ConsoleColor.Red);
                //Console.ReadLine();
                //Environment.Exit(-1);
            }

            //_logInEvent.Set();
        }

        void PlaylistContainer_Loaded(object sender, EventArgs e)
        {

            var playlistitem = Session.PlaylistContainer.Playlists.Single(i => i.Name == "Rock Classics");

            var playlist = new Playlist(playlistitem, Application.Current.Dispatcher);

            Console.WriteLine(playlist.Name);

            var tracklist = new List<Spotify.Client.Infrastructure.Interfaces.ITrack>();

            foreach (ITrack track in playlist.InternalPlaylist.Tracks)
            {

                var thetrack = new Track(track, Application.Current.Dispatcher);

                if (thetrack.IsAvailable)
                    tracklist.Add(thetrack);

            }

            playerController.Playlist.Set(tracklist);

        }

        private void OnSearchFinishedLoading(object sender, EventArgs e)
        {
            Spotify.Client.Infrastructure.Interfaces.ISearch search = (Spotify.Client.Infrastructure.Interfaces.ISearch)sender;

            playerController = new PlayerController(Session, Player, Application.Current.Dispatcher, logging, null);

            var album = search.Albums[0];

            Console.WriteLine(album.Name);

        }


        private void ConnectionError(object sender, SessionEventArgs e)
        {
            if (e.Status != Error.OK)
            {
                Console.WriteLine("Connection error: " + e.Message, ConsoleColor.Red);
            }
        }


        private void OnLoginComplete(object sender, SessionEventArgs e)
        {
            Console.WriteLine(Session.ConnectionState);
            Console.WriteLine(e.Status.GetMessage());

            if (e.Status != Error.OK)
            {
                Console.WriteLine(e.Status.GetMessage());
            }
            else
            {
                playerController = new PlayerController(Session, Player, Application.Current.Dispatcher, logging, null);


                var toplistBrowse = ToplistsMenu();

                ////ILink link = Session.FromLink("");


                ////ITrackAndOffset track = ((ILink<ITrackAndOffset>)link).Object;
                ////Track torshifyTrack = new Track(track.Track, Application.Current.Dispatcher);

                ////List<Track> tracks = new List<Track>();
                ////tracks.Add(torshifyTrack);


                ////Player.Playlist.Set(torshifyTrack);


                //List<ITrack> tracksToadd = new List<ITrack>();

                //for (int i = index; i < list.Tracks.Count(); i++)
                //{
                //    IPlaylistTrack item = list.Tracks.ElementAt(i);

                //    if (item.IsAvailable)
                //    {
                //        tracksToadd.Add(item);
                //    }
                //}

                //var list = Session.BrowseCurrentUser(ToplistType.Tracks, null);

                //var playerQueue = new PlayerQueue(Application.Current.Dispatcher);

                //playerQueue.Enqueue(list);



                //playerController.Playlist.Set(playerQueue.Left.FirstOrDefault().Track);

                List<Spotify.Client.Infrastructure.Interfaces.ITrack> tracks = new List<Spotify.Client.Infrastructure.Interfaces.ITrack>();

                for (int i = 0; i < toplistBrowse.Tracks.Count; i++)
                {
                    Track track = new Track(toplistBrowse.Tracks[i], Application.Current.Dispatcher);

                    tracks.Add(track);

                }

                playerController.Playlist.Set(tracks);
                playerController.Play();

                Session.LoginComplete -= OnLoginComplete;
            }
        }

        protected IToplistBrowse ToplistsMenu()
        {
            Console.WriteLine("=== Toplist ===", ConsoleColor.Cyan);
            IToplistBrowse toplistBrowse = Session
                                            .Browse(ToplistType.Tracks)
                                            .WaitForCompletion();

            for (int i = 0; i < toplistBrowse.Tracks.Count; i++)
            {
                ITrack track = toplistBrowse.Tracks[i];

                Console.Write("{0:00} : {1,-20}", ConsoleColor.White, (i + 1), track.Name);
                Console.Write(" {0,-16}", ConsoleColor.Gray, track.Album.Artist.Name);
                Console.WriteLine(" {0,-16}", ConsoleColor.DarkGray, track.Album.Name);
            }

            return toplistBrowse;
        }

        public static ILog BootLogger;
        private bool CanChangePlayState=true;


        private void InitializeLogging()
        {
            var fileAppender = new RollingFileAppender();
            fileAppender.File = System.IO.Path.Combine(AppConstants.LogFolder, "Torshify.log");
            fileAppender.AppendToFile = true;
            fileAppender.MaxSizeRollBackups = 10;
            fileAppender.MaxFileSize = 1024 * 1024;
            fileAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
            fileAppender.StaticLogFileName = true;
            fileAppender.Layout = new PatternLayout("%date{dd MMM yyyy HH:mm} [%thread] %-5level %logger - %message%newline");
            fileAppender.Threshold = Level.Info;
            fileAppender.ActivateOptions();

            var consoleAppender = new CustomConsoleColorAppender();
            consoleAppender.AddMapping(
                new CustomConsoleColorAppender.LevelColors
                {
                    ForeColor = ColoredConsoleAppender.Colors.White | ColoredConsoleAppender.Colors.HighIntensity,
                    BackColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity,
                    Level = Level.Fatal
                });
            consoleAppender.AddMapping(
                new CustomConsoleColorAppender.LevelColors
                {
                    ForeColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity,
                    Level = Level.Error
                });
            consoleAppender.AddMapping(
                new CustomConsoleColorAppender.LevelColors
                {
                    ForeColor = ColoredConsoleAppender.Colors.Yellow | ColoredConsoleAppender.Colors.HighIntensity,
                    Level = Level.Warn
                });
            consoleAppender.AddMapping(
                new CustomConsoleColorAppender.LevelColors
                {
                    ForeColor = ColoredConsoleAppender.Colors.Green | ColoredConsoleAppender.Colors.HighIntensity,
                    Level = Level.Info
                });
            consoleAppender.AddMapping(
                new CustomConsoleColorAppender.LevelColors
                {
                    ForeColor = ColoredConsoleAppender.Colors.White | ColoredConsoleAppender.Colors.HighIntensity,
                    Level = Level.Info
                });
            consoleAppender.Layout = new PatternLayout("%date{dd MM HH:mm} %-5level - %message%newline");
#if DEBUG
            consoleAppender.Threshold = Level.All;
#else
            consoleAppender.Threshold = Level.Info;
#endif
            consoleAppender.ActivateOptions();

            Logger root;
            root = ((Hierarchy)LogManager.GetRepository()).Root;
            root.AddAppender(consoleAppender);
            root.AddAppender(fileAppender);
            root.Repository.Configured = true;

            BootLogger = LogManager.GetLogger("Bootstrapper");

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Exception exception = (Exception)e.ExceptionObject;
                BootLogger.Fatal(exception);
            };

            Application.Current.Dispatcher.UnhandledException += (s, e) =>
            {
                Exception exception = e.Exception;
                BootLogger.Fatal(exception);
            };
        }

    }
}