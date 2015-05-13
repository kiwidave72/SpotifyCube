using System;

namespace Spotify
{
    public class SpotifyException : Exception
    {
        #region Constructors

        public SpotifyException(string message, Error error)
            : base(message)
        {
            Error = error;
        }

        #endregion Constructors

        #region Properties

        public Error Error
        {
            get; private set;
        }

        #endregion Properties
    }
}