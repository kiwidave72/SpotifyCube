namespace Spotify.Client.Infrastructure.Interfaces
{
    public interface ITab<TModel>
    {
        ITabViewModel<TModel> ViewModel
        {
            get;
        }
    }
}