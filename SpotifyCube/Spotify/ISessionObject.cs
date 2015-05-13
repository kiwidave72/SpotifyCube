using System;

namespace Spotify
{
    public interface ISessionObject : IDisposable
    {
        ISession Session { get; }
    }
}