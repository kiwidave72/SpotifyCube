using System.Windows.Media.Imaging;

namespace Spotify.Client.Infrastructure.Interfaces
{
    public interface IImageCacheEntry
    {
        #region Properties

        BitmapSource Bitmap
        {
            get;
        }

        string ID
        {
            get;
        }

        bool IsLoaded
        {
            get;
        }

        #endregion Properties
    }
}