﻿using System.Windows.Controls;

using Spotify.Client.Infrastructure.Interfaces;

namespace Spotify.Client.Modules.Core.Views.WhatsNew.Tabs
{
    public partial class WhatsNewTabItemView : UserControl, ITab<WhatsNewViewModel>
    {
        #region Constructors

        public WhatsNewTabItemView(WhatsNewTabItemViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        #endregion Constructors

        #region Properties

        public ITabViewModel<WhatsNewViewModel> ViewModel
        {
            get { return DataContext as WhatsNewTabItemViewModel; }
            set { DataContext = value; }
        }

        #endregion Properties
    }
}
