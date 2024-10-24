using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Tomat.PoppedCap.CLI.Utilities;

internal static class GameResolution
{
    public static bool TryGetGameFromName(string name, [NotNullWhen(returnValue: true)] out PopCapGame? game)
    {
        foreach (var knownGame in WellKnownPopCapGames.ALL_GAMES)
        {
            if (knownGame.GameName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                game = knownGame;
                return true;
            }

            if (knownGame.Aliases.Any(x => x.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                game = knownGame;
                return true;
            }
        }

        game = null;
        return false;
    }
}