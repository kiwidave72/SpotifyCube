using System.Windows.Controls;
using Spotify.Client.Infrastructure.Interfaces;

namespace Spotify.Client.Modules.Core.Views.Artist.Tabs
{
    public partial class BiographyTabItemView : UserControl, ITab<IArtist>
    {
        #region Constructors

        public BiographyTabItemView(BiographyTabItemViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }

        #endregion Constructors

        #region Properties

        public ITabViewModel<IArtist> ViewModel
        {
            get { return DataContext as BiographyTabItemViewModel; }
            set { DataContext = value; }
        }

        #endregion Properties
    }
}