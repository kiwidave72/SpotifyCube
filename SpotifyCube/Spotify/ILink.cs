using System;

namespace Spotify
{
    public interface ILink : IDisposable
    {
        LinkType Type
        {
            get;
        }

        object Object
        {
            get;
        }

        string GetStringLink();
    }
}
