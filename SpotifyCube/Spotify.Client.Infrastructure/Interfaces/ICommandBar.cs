using System.Windows.Input;
using Spotify.Client.Infrastructure.Models;

namespace Spotify.Client.Infrastructure.Interfaces
{
    public interface ICommandBar
    {
        #region Methods

        ICommandBar AddCommand(string displayName, ICommand command);

        ICommandBar AddCommand(string displayName, ICommand command, object commandParameter);

        ICommandBar AddCommand(CommandModel commandModel);

        ICommandBar AddSeparator();

        ICommandBar AddSeparator(string displayName);

        ICommandBar AddSubmenu(string displayName);

        #endregion Methods
    }
}