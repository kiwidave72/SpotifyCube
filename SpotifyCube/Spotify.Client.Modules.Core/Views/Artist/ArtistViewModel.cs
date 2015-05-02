using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Spotify.Client.Infrastructure.Interfaces;
using Spotify.Client.Infrastructure.Models;
using Microsoft.Practices.Prism;
using Spotify.Client.Modules.Core.Views.Artist.Tabs;

namespace Spotify.Client.Modules.Core.Views.Artist
{
    public class ArtistViewModel : TabViewModel<IArtist>, IPartImportsSatisfiedNotification
    {
        #region Fields

        [ImportMany]
        private IEnumerable<Lazy<ITab<IArtist>>> _tabImports = null;

        #endregion Fields

        #region Constructors

        public ArtistViewModel(CompositionContainer mefContainer)
        {
            AddTab(ServiceLocator.Current.TryResolve<OverviewTabItemView>());
            AddTab(ServiceLocator.Current.TryResolve<BiographyTabItemView>());

            mefContainer.SatisfyImportsOnce(this);
        }

        #endregion Constructors

        #region Methods

        protected override IArtist GetModel(NavigationContext navigationContext)
        {
            return navigationContext.Tag as IArtist;
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            foreach (var tabImport in _tabImports)
            {
                AddTab(tabImport.Value);
            }
        }

        #endregion Methods
    }
}