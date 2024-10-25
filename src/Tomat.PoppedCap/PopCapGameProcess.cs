using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tomat.PoppedCap;

/// <summary>
///     A simple wrapper around a <see cref="PopCapGame"/> for installing and
///     launching a given game as well as performing common tasks on it.
/// </summary>
/// <param name="Game">The game to wrap and handle.</param>
/// <param name="FilePath">The path to the installer executable.</param>
public readonly record struct PopCapGameProcess(PopCapGame Game, string FilePath)
{
    public delegate void DownloadProgressHandler(long bytesReceived, long totalBytesToReceive);

    private static readonly HttpClient client = new();

    /// <summary>
    ///     Downloads the game.
    /// </summary>
    /// <param name="game">
    ///     The game to download.
    /// </param>
    /// <param name="progressHandler">Handles progress updates.</param>
    /// <param name="fileName">
    ///     The file name to install the game to. If unspecified, it is
    ///     installed to a temporary location.
    /// </param>
    /// <returns>
    ///     The <see cref="PopCapGameProcess"/> wrapper, or null if the download
    ///     failed.
    /// </returns>
    public static async Task<PopCapGameProcess?> DownloadAsync(
        PopCapGame               game,
        DownloadProgressHandler? progressHandler,
        string?                  fileName = null
    )
    {
        fileName ??= Path.GetTempFileName();

        var request  = new HttpRequestMessage(HttpMethod.Get, game.GameUrl);
        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var totalBytes    = response.Content.Headers.ContentLength;
        var bytesReceived = 0;
        var buffer        = new byte[8192];
        Debug.Assert(totalBytes.HasValue);

        await using var fileStream     = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
        await using var responseStream = await response.Content.ReadAsStreamAsync();

        var isMoreToRead = true;
        do
        {
            var read = await responseStream.ReadAsync(buffer);
            if (read == 0)
            {
                isMoreToRead = false;
            }
            else
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, read));
                bytesReceived += read;
                progressHandler?.Invoke(bytesReceived, totalBytes.Value);
            }
        }
        while (isMoreToRead);

        return new PopCapGameProcess(game, fileName);
    }

    public async Task<bool> InstallAsync()
    {
        try
        {
            var process = Process.Start(
                new ProcessStartInfo
                {
                    FileName        = "cmd.exe",
                    Arguments       = $"/c \"{FilePath}\"",
                    UseShellExecute = true,
                    Verb            = "runas", // TODO: Windows-only elevation.
                }
            );
            Debug.Assert(process is not null);
            await process.WaitForExitAsync();
            return process.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }
}