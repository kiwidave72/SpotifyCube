namespace Spotify
{
    public interface IUser : ISessionObject
    {
        #region Properties

        string CanonicalName
        {
            get;
        }

        string DisplayName
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