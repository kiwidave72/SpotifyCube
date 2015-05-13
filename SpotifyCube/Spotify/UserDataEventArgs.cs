using System;

namespace Spotify
{
    public class UserDataEventArgs : EventArgs
    {
        public UserDataEventArgs(object tag)
        {
            Tag = tag;
        }

        public object Tag { get; private set; }
    }
}