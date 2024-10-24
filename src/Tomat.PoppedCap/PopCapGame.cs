namespace Tomat.PoppedCap;

/// <summary>
///     A simple, complete description of a PopCap game.
///     <br />
///     Provides necessary information for installing and managing locally.
/// </summary>
/// <param name="GameName">The human-readable name of the game.</param>
/// <param name="GameUrl">The URL of the game installer.</param>
/// <param name="Aliases">Simple aliases for interfacing with the CLI.</param>
public readonly record struct PopCapGame(
    string   GameName,
    string   GameUrl,
    string[] Aliases
);