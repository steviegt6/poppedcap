using System.Threading.Tasks;

using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

using Spectre.Console;

using Tomat.PoppedCap.CLI.Utilities;

namespace Tomat.PoppedCap.CLI.Commands;

[Command("list-games", Description = "Lists known PopCap games")]
public class ListGamesCommand : ICommand
{
    ValueTask ICommand.ExecuteAsync(IConsole console)
    {
        foreach (var game in WellKnownPopCapGames.ALL_GAMES)
        {
            AnsiConsole.MarkupLine($"• [aqua bold]{FormattingHelpers.PadGameName(game.GameName)}[/] [grey italic]({game.GameUrl})[/]");
        }

        return default;
    }
}