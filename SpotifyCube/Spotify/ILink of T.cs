namespace Spotify
{
    public interface ILink<out T> : ILink
    {
        new T Object { get; }
    }
}