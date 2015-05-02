using System.Windows;
using System.Windows.Controls;

using Spotify.Client.Infrastructure.Controls;
using Spotify.Client.Modules.EchoNest.Controls;

namespace Spotify.Client.Modules.EchoNest.Views.Similar
{
    public class NodeTemplateSelector : DataTemplateSelector
    {
        #region Fields

        private Graph _graph;

        #endregion Fields

        #region Properties

        public DataTemplate CenterNodeTemplate
        {
            get; set;
        }

        public DataTemplate ChildNodeTemplate
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (_graph == null)
            {
                _graph = UIHelpers.FindVisualParent<Graph>(container);
            }

            if (_graph != null && _graph.CenterObject == item)
            {
                return CenterNodeTemplate;
            }

            return ChildNodeTemplate;
        }

        #endregion Methods
    }
}