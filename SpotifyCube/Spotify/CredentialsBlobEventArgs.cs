using System;

namespace Spotify
{
    public class CredentialsBlobEventArgs : EventArgs
    {
        public CredentialsBlobEventArgs(string blob)
        {
            Blob = blob;
        }

        public string Blob
        {
            get;
            private set;
        }
    }
}