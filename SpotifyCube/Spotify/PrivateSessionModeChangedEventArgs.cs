using System;

namespace Spotify
{
    public class PrivateSessionModeChangedEventArgs : EventArgs
    {
        public PrivateSessionModeChangedEventArgs(bool isPrivate)
        {
            IsPrivate = isPrivate;
        }

        public bool IsPrivate
        {
            get;
            private set;
        }
    }
}