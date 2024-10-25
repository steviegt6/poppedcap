using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tomat.PoppedCap.CLI.Utilities;

internal readonly record struct GameNetStatus(bool Success, HttpStatusCode Code);

internal static class NetHelpers
{
    private static readonly HttpClient client = new();

    /// <summary>
    ///     Checks the status of a game's installer through its URL.
    /// </summary>
    /// <param name="game">The game.</param>
    /// <returns>
    ///     Whether the file is accessible as well as its response status code.
    /// </returns>
    public static async Task<GameNetStatus> GetGameStatusAsync(PopCapGame game)
    {
        var request  = new HttpRequestMessage(HttpMethod.Head, game.GameUrl);
        var response = await client.SendAsync(request);
        return new GameNetStatus(response.IsSuccessStatusCode, response.StatusCode);
    }
}