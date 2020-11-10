using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetCode2.Server.Realtime.Application;

namespace NetCode2.Server.Console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.Title = "NetCode2";

            var executingAssembly = Assembly.GetExecutingAssembly();

            Task realtimeServer = RealtimeServer
                .Create(Path.GetDirectoryName(executingAssembly.Location), args)
                .RunConsoleAsync();

            await Task.WhenAll(realtimeServer);
        }
    }
}