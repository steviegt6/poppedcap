using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

using Spectre.Console;

using Tomat.PoppedCap.CLI.Utilities;

namespace Tomat.PoppedCap.CLI.Commands;

// TODO: Option to select a game interactively (prompt).

[Command("check-status", Description = "Checks the status of game setups online")]
public class CheckStatusCommand : ICommand
{
    private const string result_description = "[grey]Checking[/] {0}[grey]...[/]";
    private const string result_success     = "[green]✅[/]";
    private const string result_failure     = "[red]❌[/] [grey]({0})[/]";

    [CommandParameter(0, Description = "The name of the game (full or alias) or \"all\"")]
    public string GameName { get; set; } = string.Empty;

    async ValueTask ICommand.ExecuteAsync(IConsole console)
    {
        GameName = GameName.ToLowerInvariant();

        if (GameName == "all")
        {
            await CheckGameStatuses(WellKnownPopCapGames.ALL_GAMES);
        }
        else if (GameResolution.TryGetGameFromName(GameName, out var game))
        {
            await CheckGameStatuses([game.Value]);
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Unknown game: {GameName}[/]");

            // TODO: Attempt to find closest match (levenshtein distance)?
        }
    }

    private static async Task CheckGameStatuses(PopCapGame[] games)
    {
        await AnsiConsole.Progress()
                         .AutoClear(false)
                         .HideCompleted(false)
                         .Columns(new TaskDescriptionColumn { Alignment = Justify.Left }, new ProgressBarColumn(), new SpinnerColumn())
                         .StartAsync(
                              async x =>
                              {
                                  foreach (var game in games)
                                  {
                                      var task = x.AddTask(GetPaddedDescription(game.GameName, null));
                                      task.IsIndeterminate = true;

                                      var (success, status) = await CheckGameStatus(game);
                                      task.IsIndeterminate  = false;
                                      task.Value            = 100;
                                      task.Description      = GetPaddedDescription(game.GameName, (success, (int)status));
                                  }
                              }
                          );
    }

    private static async Task<(bool success, HttpStatusCode status)> CheckGameStatus(PopCapGame game)
    {
        // TODO: Move HttpClient out... general utilities...
        var client   = new HttpClient();
        var response = await client.GetAsync(game.GameUrl);
        return (response.IsSuccessStatusCode, response.StatusCode);
    }

    private static string GetPaddedDescription(string gameName, (bool success, int status)? result)
    {
        var description = string.Format(result_description, gameName);
        if (result is not null)
        {
            description += " ";
            description += result.Value.success ? result_success : string.Format(result_failure, result.Value.status.ToString("D3"));
        }

        return description.PadRight(FormattingHelpers.MaxGameNameLength + (result_description.Length - 3) + 1 + string.Format(result_failure, "000").Length);
    }
}