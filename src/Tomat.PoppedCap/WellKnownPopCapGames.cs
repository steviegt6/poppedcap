namespace Tomat.PoppedCap;

/// <summary>
///     Defines a well-known collection of supported PopCap games.
/// </summary>
public static class WellKnownPopCapGames
{
    private static readonly PopCapGame alchemy                         = MakeGame("Alchemy",                          ["alchemy"]);
    private static readonly PopCapGame astro_pop                       = MakeGame("Astro Pop",                        ["astropop"]);
    private static readonly PopCapGame atomica                         = MakeGame("Atomica",                          ["atomica"]);
    private static readonly PopCapGame banana_bugs                     = MakeGame("Banana Bugs",                      ["bananabugs"]);
    private static readonly PopCapGame bejeweled                       = MakeGame("Bejeweled",                        ["bejeweled"]);
    private static readonly PopCapGame bejeweled_2                     = MakeGame("Bejeweled 2",                      ["bejeweled2"]);
    private static readonly PopCapGame bejeweled_3                     = MakeGame("Bejeweled 3",                      ["bejeweled3"]);
    private static readonly PopCapGame bejeweled_twist                 = MakeGame("Bejeweled Twist",                  ["bejeweledtwist"]);
    private static readonly PopCapGame big_money                       = MakeGame("Big Money",                        ["bigmoney"]);
    private static readonly PopCapGame bonnies_bookstore               = MakeGame("Bonnie's Bookstore",               ["bonniesbookstore"]);
    private static readonly PopCapGame bookworm                        = MakeGame("Bookworm",                         ["bookworm"]);
    private static readonly PopCapGame bookworm_adventures             = MakeGame("Bookworm Adventures",              ["bwa"],     "BWASetup");
    private static readonly PopCapGame bookworm_adventures_volume_2    = MakeGame("Bookworm Adventures Volume 2",     ["bwavol2"], "BWAVol2Setup");
    private static readonly PopCapGame chuzzle                         = MakeGame("Chuzzle",                          ["chuzzle"]);
    private static readonly PopCapGame dynomite                        = MakeGame("Dynomite",                         ["dynomite"]);
    private static readonly PopCapGame feeding_frenzy                  = MakeGame("Feeding Frenzy",                   ["feedingfrenzy"]);
    private static readonly PopCapGame hammer_heads                    = MakeGame("Hammer Heads",                     ["hammerheads"]);
    private static readonly PopCapGame insaniquarium                   = MakeGame("Insaniquarium",                    ["insaniquarium"]);
    private static readonly PopCapGame noahs_ark                       = MakeGame("Noah's Ark",                       ["noahsark"]);
    private static readonly PopCapGame peggle                          = MakeGame("Peggle",                           ["peggle"]);
    private static readonly PopCapGame peggle_nights                   = MakeGame("Peggle Nights",                    ["pegglenights"]);
    private static readonly PopCapGame pixelus                         = MakeGame("Pixelus",                          ["pixelus"]);
    private static readonly PopCapGame plants_vs_zombies               = MakeGame("Plants Vs. Zombies",               ["pvz", "plantsvszombies"]);
    private static readonly PopCapGame talismania                      = MakeGame("Talismania",                       ["talismania"]);
    private static readonly PopCapGame tip_top                         = MakeGame("Tip Top",                          ["tiptop"]);
    private static readonly PopCapGame typer_shark                     = MakeGame("Typer Shark",                      ["typershark"]);
    private static readonly PopCapGame vacation_quest_australia        = MakeGame("Vacation Quest: Australia",        ["vacationaustralia"], "VacationQuestAustraliaSetup_20120801");
    private static readonly PopCapGame vacation_quest_hawaiian_islands = MakeGame("Vacation Quest: Hawaiian Islands", ["vacationhawaii"],    "VacationQuestHawaiiSetup");
    private static readonly PopCapGame venice                          = MakeGame("Venice",                           ["venice"]);
    private static readonly PopCapGame word_harmony                    = MakeGame("Word Harmony",                     ["wordharmony"]);
    private static readonly PopCapGame zuma                            = MakeGame("Zuma",                             ["zuma"]);
    private static readonly PopCapGame zumas_revenge                   = MakeGame("Zuma's Revenge!",                  ["zumasrevenge"]);

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

    private static PopCapGame MakeGame(string gameName, string[] aliases, string? setupName = null)
    {
        return new PopCapGame(gameName, MakeGameUrl(setupName ?? InferSetupName(gameName)), aliases);
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