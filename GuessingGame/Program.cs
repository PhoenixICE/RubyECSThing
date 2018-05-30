using System;
using System.Threading.Tasks;
using GuessingGame.Res;
using GuessingGame.System.Game;
using GuessingGame.System.Input;
using GuessingGame.System.State;
using SharperUniverse.Core;

namespace GuessingGame
{
    class Program
    {
        //Can't inject this
        public static GameRunner GameRunner;

        static async Task Main(string[] args)
        {
            var commandRunner = new UniverseCommandRunner()
                .AddCommandBinding<ExploreCommandBinding>("explore");

            GameRunner = new GameRunner(commandRunner, new ConsoleIOHandler(), 50);

            await SetupSystems();

            await GameRunner.RunGameAsync();
        }

        public async static Task SetupSystems()
        {
            var playerLocationSystem = new PlayerLocationSystem(GameRunner);
            var mapSystem =  new MapSystem(GameRunner);
            var exploreInputSystem = new ExploreInputSystem(GameRunner);
            var explorableSystem = new ExplorableSystem(GameRunner);

            bool setPlayerLocation = false;
            foreach (var map in GameConfig.Locations)
            {
                var mapEntity = await GameRunner.CreateEntityAsync();

                if (!setPlayerLocation)
                {
                    await Console.Out.WriteLineAsync(map.Value.Description);
                    await playerLocationSystem.RegisterComponentAsync(mapEntity);
                    setPlayerLocation = true;
                }

                await mapSystem.RegisterComponentAsync(mapEntity);

                foreach (var location in map.Value.ExplorableLocations)
                {
                    await explorableSystem.RegisterComponentAsync(mapEntity, GameConfig.Locations[location]);
                }
            }
        }
    }

    public static class UniverseCommandRunnerExtensions
    {
        public static UniverseCommandRunner AddCommandBinding<T>(this UniverseCommandRunner self, string commandName) where T : IUniverseCommandBinding
        {
            self.AddCommandBinding((IUniverseCommandBinding)Activator.CreateInstance(typeof(T), commandName));
            return self;
        }
    }
}