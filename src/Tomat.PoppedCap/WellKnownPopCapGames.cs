namespace Tomat.PoppedCap;

/// <summary>
///     Defines a well-known collection of supported PopCap games.
/// </summary>
public static class WellKnownPopCapGames
{
    private static readonly PopCapGame alchemy                         = MakeGame("Alchemy");
    private static readonly PopCapGame astro_pop                       = MakeGame("Astro Pop");
    private static readonly PopCapGame atomica                         = MakeGame("Atomica");
    private static readonly PopCapGame banana_bugs                     = MakeGame("Banana Bugs");
    private static readonly PopCapGame bejeweled                       = MakeGame("Bejeweled");
    private static readonly PopCapGame bejeweled_2                     = MakeGame("Bejeweled 2");
    private static readonly PopCapGame bejeweled_3                     = MakeGame("Bejeweled 3");
    private static readonly PopCapGame bejeweled_twist                 = MakeGame("Bejeweled Twist");
    private static readonly PopCapGame big_money                       = MakeGame("Big Money");
    private static readonly PopCapGame bonnies_bookstore               = MakeGame("Bonnie's Bookstore");
    private static readonly PopCapGame bookworm                        = MakeGame("Bookworm");
    private static readonly PopCapGame bookworm_adventures             = MakeGame("Bookworm Adventures",          "BWASetup");
    private static readonly PopCapGame bookworm_adventures_volume_2    = MakeGame("Bookworm Adventures Volume 2", "BWAVol2Setup");
    private static readonly PopCapGame chuzzle                         = MakeGame("Chuzzle");
    private static readonly PopCapGame dynomite                        = MakeGame("Dynomite");
    private static readonly PopCapGame feeding_frenzy                  = MakeGame("Feeding Frenzy");
    private static readonly PopCapGame hammer_heads                    = MakeGame("Hammer Heads");
    private static readonly PopCapGame insaniquarium                   = MakeGame("Insaniquarium");
    private static readonly PopCapGame noahs_ark                       = MakeGame("Noah's Ark");
    private static readonly PopCapGame peggle                          = MakeGame("Peggle");
    private static readonly PopCapGame peggle_nights                   = MakeGame("Peggle Nights");
    private static readonly PopCapGame pixelus                         = MakeGame("Pixelus");
    private static readonly PopCapGame plants_vs_zombies               = MakeGame("Plants Vs. Zombies");
    private static readonly PopCapGame talismania                      = MakeGame("Talismania");
    private static readonly PopCapGame tip_top                         = MakeGame("Tip Top");
    private static readonly PopCapGame typer_shark                     = MakeGame("Typer Shark");
    private static readonly PopCapGame vacation_quest_australia        = MakeGame("Vacation Quest: Australia",        "VacationQuestAustraliaSetup_20120801");
    private static readonly PopCapGame vacation_quest_hawaiian_islands = MakeGame("Vacation Quest: Hawaiian Islands", "VacationQuestHawaiiSetup");
    private static readonly PopCapGame venice                          = MakeGame("Venice");
    private static readonly PopCapGame word_harmony                    = MakeGame("Word Harmony");
    private static readonly PopCapGame zuma                            = MakeGame("Zuma");
    private static readonly PopCapGame zumas_revenge                   = MakeGame("Zuma's Revenge!");

    public static readonly PopCapGame[] ALL_GAMES =
    [
        alchemy,
        astro_pop,
        atomica,
        banana_bugs,
        bejeweled,
        bejeweled_2,
        bejeweled_3,
        bejeweled_twist,
        big_money,
        bonnies_bookstore,
        bookworm,
        bookworm_adventures,
        bookworm_adventures_volume_2,
        chuzzle,
        dynomite,
        feeding_frenzy,
        hammer_heads,
        insaniquarium,
        noahs_ark,
        peggle,
        peggle_nights,
        pixelus,
        plants_vs_zombies,
        talismania,
        tip_top,
        typer_shark,
        vacation_quest_australia,
        vacation_quest_hawaiian_islands,
        venice,
        word_harmony,
        zuma,
        zumas_revenge,
    ];

    private static PopCapGame MakeGame(string gameName, string? setupName = null)
    {
        return new PopCapGame(gameName, MakeGameUrl(setupName ?? InferSetupName(gameName)));
    }

    private static string MakeGameUrl(string setupName)
    {
        return $"https://static-www.ec.popcap.com/binaries/popcap_downloads/{setupName}.exe";
    }

    private static string InferSetupName(string gameName)
    {
        gameName = gameName.Replace(" ", "");
        gameName = gameName.Replace(":", "");
        gameName = gameName.Replace("!", "");
        gameName = gameName.Replace("'", "");
        gameName = gameName.Replace(".", "");
        return gameName + "Setup";
    }
}