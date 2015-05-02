using Microsoft.Practices.Prism.Commands;

namespace Spotify.Client.Modules.Core
{
    public static class CoreCommands
    {
        public static class Player
        {
            public static readonly CompositeCommand PauseCommand = new CompositeCommand();
            public static readonly CompositeCommand PlayCommand = new CompositeCommand();
            public static readonly CompositeCommand PlayPauseCommand = new CompositeCommand();
            public static readonly CompositeCommand NextCommand = new CompositeCommand();
            public static readonly CompositeCommand PreviousCommand = new CompositeCommand();
            public static readonly CompositeCommand SeekCommand = new CompositeCommand();
            public static readonly CompositeCommand ToggleRepeatCommand = new CompositeCommand();
            public static readonly CompositeCommand ToggleShuffleCommand = new CompositeCommand();
        }

        public static class Views
        {
            public static readonly CompositeCommand GoToArtistCommand = new CompositeCommand();
            public static readonly CompositeCommand GoToAlbumCommand = new CompositeCommand();
        }

        public static class Debug
        {
            public static readonly CompositeCommand ToggleDebugWindowCommand = new CompositeCommand();
        }

        public static readonly CompositeCommand PlayTrackCommand = new CompositeCommand();
        public static readonly CompositeCommand QueueTrackCommand = new CompositeCommand();
        public static readonly CompositeCommand NavigateBackCommand = new CompositeCommand();
        public static readonly CompositeCommand NavigateForwardCommand = new CompositeCommand();
        public static readonly CompositeCommand SearchCommand = new CompositeCommand();
        public static readonly CompositeCommand GoToNowPlayingCommand = new CompositeCommand();
        public static readonly CompositeCommand ToggleTrackIsStarredCommand = new CompositeCommand();
    }
}