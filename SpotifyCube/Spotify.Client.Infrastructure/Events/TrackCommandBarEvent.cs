using System.Collections.Generic;

using Microsoft.Practices.Prism.Events;

using Spotify.Client.Infrastructure.Interfaces;

namespace Spotify.Client.Infrastructure.Events
{
    public class TrackCommandBarModel
    {
        #region Constructors

        public TrackCommandBarModel(ITrack track, ICommandBar commandBar)
        {
            Track = track;
            CommandBar = commandBar;
        }

        #endregion Constructors

        #region Properties

        public ITrack Track
        {
            get; private set;
        }

        public ICommandBar CommandBar
        {
            get; private set;
        }

        #endregion Properties
    }

    public class TrackCommandBarEvent : CompositePresentationEvent<TrackCommandBarModel>
    {
    }

    public class TracksCommandBarModel
    {
        #region Constructors

        public TracksCommandBarModel(IEnumerable<ITrack> tracks, ICommandBar commandBar)
        {
            Tracks = tracks;
            CommandBar = commandBar;
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<ITrack> Tracks
        {
            get; private set;
        }

        public ICommandBar CommandBar
        {
            get; private set;
        }

        #endregion Properties
    }

    public class TracksCommandBarEvent : CompositePresentationEvent<TracksCommandBarModel>
    {
    }
}