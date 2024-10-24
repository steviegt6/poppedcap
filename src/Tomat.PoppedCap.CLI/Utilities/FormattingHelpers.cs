using System;
using System.Diagnostics;
using System.Linq;

namespace Tomat.PoppedCap.CLI.Utilities;

internal static class FormattingHelpers
{
    private const string aliases_format = " [silver][[{0}]][/]";

    private static readonly Lazy<int> max_game_name_length = new(
        () =>
        {
            return WellKnownPopCapGames.ALL_GAMES.Select(x => x.GameName.Length).Prepend(0).Max();
        }
    );

    private static readonly Lazy<int> max_game_name_length_with_aliases = new(
        () =>
        {
            return WellKnownPopCapGames.ALL_GAMES.Select(x => x.GameName.Length + GetAliasText(x).Length).Prepend(0).Max();
        }
    );

    /// <summary>
    ///     Pads a game name to normalize it when displaying alongside all other
    ///     game names. Effectively, this makes it so all game names will
    ///     produce a string of the same length.
    /// </summary>
    /// <param name="game">The game.</param>
    /// <param name="padRight">
    ///     Whether the name should be padded on the right (or left).
    /// </param>
    /// <param name="withAliases">
    ///     Whether game name aliases should be included.
    /// </param>
    /// <returns>The padded name.</returns>
    public static string PadGameName(PopCapGame game, bool padRight = true, bool withAliases = true)
    {
        var gameName = game.GameName;
        if (withAliases)
        {
            var space     = max_game_name_length_with_aliases.Value - gameName.Length;
            var aliasText = GetAliasText(game);
            {
                Debug.Assert(space >= aliasText.Length, "Not enough space for alias text");
            }
            
            // Format the string such that aliasText is right-aligned:
            // GameName     [alias, alias2] (...)
            // GameNameLong        [alias1] (...)
            
            gameName = gameName.PadRight(gameName.Length + space - aliasText.Length);
            gameName += aliasText;
        }

        var length = withAliases ? max_game_name_length_with_aliases : max_game_name_length;
        return padRight ? gameName.PadRight(length.Value) : gameName.PadLeft(length.Value);
    }

    private static string GetAliasText(PopCapGame game)
    {
        return string.Format(aliases_format, string.Join(", ", game.Aliases));
    }
}