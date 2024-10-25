using System.Threading.Tasks;

using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

using Spectre.Console;

using Tomat.PoppedCap.CLI.Utilities;

namespace Tomat.PoppedCap.CLI.Commands;

[Command("install-game", Description = "Installs a game")]
public class InstallGameCommand : ICommand
{
    [CommandParameter(0, Description = "The name of the game (full or alias)")]
    public string GameName { get; set; } = string.Empty;

    async ValueTask ICommand.ExecuteAsync(IConsole console)
    {
        GameName = GameName.ToLowerInvariant();

        if (GameName == "all")
        {
            AnsiConsole.MarkupLine("[red]\"all\" is not a supported option; please select one game[/]");
        }
        else if (GameResolution.TryGetGameFromName(GameName, out var game))
        {
            await InstallGame(game.Value);
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Unknown game: {GameName}[/]");

            // TODO: Attempt to find closest match (levenshtein distance)?
        }
    }

    private static async Task InstallGame(PopCapGame game)
    {
        var process = default(PopCapGameProcess?);
        await AnsiConsole.Progress()
                         .StartAsync(
                              async x =>
                              {
                                  var task = x.AddTask($"Downloading {game.GameName}...");

                                  process = await PopCapGameProcess.DownloadAsync(
                                      game,
                                      (receivedBytes, totalBytes) =>
                                      {
                                          task.Value = receivedBytes / (double)totalBytes * 100;

                                          if (receivedBytes == totalBytes)
                                          {
                                              task.Description = $"Downloaded {game.GameName}";
                                          }
                                      }
                                  );
                              }
                          );

        if (process is null)
        {
            AnsiConsole.MarkupLine($"[red]Failed to download {game.GameName}, aborting...[/]");
            return;
        }

        var installed = false;
        await AnsiConsole.Progress()
                         .StartAsync(
                              async x =>
                              {
                                  var task = x.AddTask($"Installing {game.GameName}");
                                  task.IsIndeterminate = true;

                                  installed = await process.Value.InstallAsync();
                                  task.Description = installed
                                      ? $"Installed {game.GameName}"
                                      : $"[red]Failed to install {game.GameName}[/]";
                              }
                          );

        if (!installed)
        {
            AnsiConsole.MarkupLine($"[red]failed to install {game.GameName}, aborting...[/]");
            return;
        }
    }
}