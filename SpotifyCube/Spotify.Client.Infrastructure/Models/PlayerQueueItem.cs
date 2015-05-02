using Microsoft.Practices.Prism.ViewModel;
using Spotify.Client.Infrastructure.Interfaces;

namespace Spotify.Client.Infrastructure.Models
{
    public class PlayerQueueItem : NotificationObject
    {
        #region Constructors

        public PlayerQueueItem(bool isQueued, ITrack track)
        {
            IsQueued = isQueued;
            Track = track;
        }

        #endregion Constructors

        #region Properties

        public bool IsQueued
        {
            get;
            private set;
        }

        public ITrack Track
        {
            get;
            private set;
        }

        #endregion Properties
    }
}