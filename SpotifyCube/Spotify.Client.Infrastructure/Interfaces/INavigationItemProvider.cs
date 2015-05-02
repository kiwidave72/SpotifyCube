﻿using System.Collections.Generic;
using System.Windows;

namespace Spotify.Client.Infrastructure.Interfaces
{
    public interface INavigationItemProvider
    {
        IEnumerable<INavigationItem> Items { get; }
    }

    public interface INavigationProvider
    {
        IEnumerable<INavigationProviderItem> Items
        {
            get;
        }
    }

    public interface INavigationProviderItem
    {
        DataTemplate DataTemplate
        {
            get;
        }
    }
}