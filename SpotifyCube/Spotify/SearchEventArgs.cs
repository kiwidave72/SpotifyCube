﻿using System;

namespace Spotify
{
    public class SearchEventArgs : EventArgs
    {
        public SearchEventArgs(object tag)
        {
            Tag = tag;
        }

        public object Tag
        {
            get;
            private set;
        }
    }
}