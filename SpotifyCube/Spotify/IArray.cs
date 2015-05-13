using System.Collections.Generic;

namespace Spotify
{
    public interface IArray<T> : IEnumerable<T>
    {
        #region Properties

        int Count
        {
            get;
        }

        T this[int index]
        {
            get;
        }

        #endregion Properties

        #region Methods

        IArray<TResult> Cast<TResult>();

        #endregion Methods
    }
}