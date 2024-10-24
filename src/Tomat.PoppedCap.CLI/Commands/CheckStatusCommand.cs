using System.Collections.Generic;
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
                         .Columns(new TaskDescriptionColumn(), new ProgressBarColumn(), new SpinnerColumn())
                         .StartAsync(
                              async x =>
                              {
                                  var tasks = new List<Task>();

                                  foreach (var game in games)
                                  {
                                      var task = x.AddTask(GetDescription(game.GameName, null));
                                      task.IsIndeterminate = true;

                                      tasks.Add(
                                          Task.Run(
                                              async () =>
                                              {
                                                  var (success, status) = await CheckGameStatus(game);
                                                  task.IsIndeterminate  = false;
                                                  task.Value            = 100;
                                                  task.Description      = GetDescription(game.GameName, (success, (int)status));
                                              }
                                          )
                                      );
                                  }

                                  await Task.WhenAll(tasks);
                              }
                          );
    }

    private static async Task<(bool success, HttpStatusCode status)> CheckGameStatus(PopCapGame game)
    {
        // TODO: Move HttpClient out... general utilities...
        var client   = new HttpClient();
        var request  = new HttpRequestMessage(HttpMethod.Head, game.GameUrl);
        var response = await client.SendAsync(request);
        return (response.IsSuccessStatusCode, response.StatusCode);
    }

    private static string GetDescription(string gameName, (bool success, int status)? result)
    {
        var description = $"[grey]Checking[/] {gameName}[grey]...[/]";
        if (result is null)
        {
            return description;
        }

        description += " ";
        description += result.Value.success ? "[green]✅[/]" : $"[red]❌[/] [grey]({result.Value.status:D3})[/]";
        return description;
    }
}