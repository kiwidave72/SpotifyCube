using System;

namespace Spotify.Core
{
    internal interface INativeObject : ISessionObject
    {
        IntPtr Handle { get; }
    }
}