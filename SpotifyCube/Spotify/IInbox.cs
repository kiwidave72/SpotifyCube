using System;

namespace Spotify
{
    public interface IInbox
    {
        event EventHandler Complete;

        Error Error { get; }
    }
}