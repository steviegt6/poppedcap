# poppedcap

**poppedcap** is a simple CLI tool for installing and unlocking PopCap games. It programmatically reproduces the instructions [listed here](https://matthewhinchy.com/blogs/main/2021/03/04/direct-links-to-popcaps-games-still-work.htm).

I haven't extensively tested this tool. I have confirmed it performs the steps fine for Plants vs. Zombies but no other games (they should work in theory but may require hardcoding some paths/names).

I also haven't confirmed that this removes the stage lock for the PvZ demo (or whether it's even included in this version). If it doesn't, it should be an easy fix.

## why?

There are a lot of no-longer accessible games/versions here with no way to legally purchase them.

## instructions

Build the latest binary and run `poppedcap.exe install-game <game-name-or-alias>`.

You can see all game names and their aliases with `poppedcap.exe list-games -a`.

When installing a game:

1. Let it download the file.
2. Confirm privilege escalation when prompted to allow it to run the installer.
3. Go through the installation process.
4. Run the game from the game's launcher.
5. Confirm privilege escalation to `taskkill` the launcher and game.
6. Let the installation finish.

Once this is completed, you should have access to the full game bypassing any time trials.
