using System;
using System.Linq;

namespace Tomat.PoppedCap.CLI.Utilities;

internal static class FormattingHelpers
{
    private static readonly Lazy<int> max_game_name_length = new(
        () =>
        {
            return WellKnownPopCapGames.ALL_GAMES.Select(x => x.GameName.Length).Prepend(0).Max();
        }
    );

    /// <summary>
    ///     Pads a game name to normalize it when displaying alongside all other
    ///     game names. Effectively, this makes it so all game names will
    ///     produce a string of the same length.
    /// </summary>
    /// <param name="gameName">The human-readable name of the game.</param>
    /// <param name="padRight">
    ///     Whether the name should be padded on the right (or left).
    /// </param>
    /// <returns>The padded name.</returns>
    public static string PadGameName(string gameName, bool padRight = true)
    {
        return padRight
            ? gameName.PadRight(max_game_name_length.Value)
            : gameName.PadLeft(max_game_name_length.Value);
    }
}