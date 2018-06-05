using System;
using System.Threading.Tasks;
using GuessingGame.System.Game;
using SharperUniverse.Core;

namespace GuessingGame
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var builder = new GameBuilder()
                    .AddCommand<ExploreCommandInfo>("explore")
                    .AddIOHandler<ConsoleIOHandler>()
                    .AddSystem<LocationSystem>()
                    .ComposeSystems()
                    .ComposeEntities()
                    .Build();

                await builder.StartGameAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
}