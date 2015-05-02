using System.Windows.Controls;

namespace Spotify.Client.Modules.Core.Views.Artist
{
    public partial class ArtistView : UserControl
    {
        #region Constructors

        public ArtistView(ArtistViewModel viewModel)
        {
            InitializeComponent();
            Model = viewModel;
        }

        #endregion Constructors

        #region Properties

        public ArtistViewModel Model
        {
            get { return DataContext as ArtistViewModel; }
            set { DataContext = value; }
        }

        #endregion Properties
    }
}