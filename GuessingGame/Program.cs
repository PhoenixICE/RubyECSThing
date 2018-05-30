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
                //await Console.Out.WriteLineAsync($"Creating Map: {map.Value.Name}");

                if (!setPlayerLocation)
                {
                    await Console.Out.WriteLineAsync(GameConfig.InitialGameMessage);
                    await playerLocationSystem.RegisterComponentAsync(mapEntity);
                    setPlayerLocation = true;
                }

                await mapSystem.RegisterComponentAsync(mapEntity, map.Value);

                foreach (var locationID in map.Value.ExplorableLocations)
                {
                    var location = GameConfig.Locations[locationID];
                    await explorableSystem.RegisterComponentAsync(mapEntity, location);
                    //await Console.Out.WriteLineAsync($"{map.Value.Name}: Added Explorable Location {location.Name}");
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