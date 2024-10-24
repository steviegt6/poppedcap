using System.Threading.Tasks;

using CliFx;

namespace Tomat.PoppedCap.CLI;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await new CliApplicationBuilder()
                    .SetTitle("poppedcap")
                    .SetDescription("cli utility for managing popcap games")
                    .AddCommandsFromThisAssembly()
                    .Build()
                    .RunAsync(args);
    }
}