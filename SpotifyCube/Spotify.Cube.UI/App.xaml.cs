using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Spotify.Client.Spotify;

namespace Spotify.Cube.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        #region Constructors

        public App()
        {
            SpotifyModule.InitializeLibspotify();
        }

        #endregion Constructors

        #region Properties

        protected Bootstrapper Bootstrapper
        {
            get;
            private set;
        }

        #endregion Properties

        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper = new Bootstrapper();
            Bootstrapper.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (Bootstrapper != null)
            {
                Bootstrapper.Dispose();
                Bootstrapper = null;
            }

            base.OnExit(e);
        }

        #endregion Protected Methods
    }
}
