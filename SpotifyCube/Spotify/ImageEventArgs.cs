using System;

namespace Spotify
{
    public class ImageEventArgs : EventArgs
    {
        #region Constructors

        public ImageEventArgs(string imageId)
        {
            ImageId = imageId;
        }

        #endregion Constructors

        #region Properties

        public string ImageId
        {
            get; private set;
        }

        #endregion Properties
    }
}