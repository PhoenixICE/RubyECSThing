using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessingGame.Component.Game;
using GuessingGame.Res;
using SharperUniverse.Core;

namespace GuessingGame.System.Game
{
    public class LocationSystem : BaseSharperSystem<LocationComponent>
    {
        private readonly SharperInputSystem _inputSystem;

        public LocationSystem(GameRunner game, SharperInputSystem inputSystem) : base(game)
        {
            _inputSystem = inputSystem;

            foreach (var location in GameConfig.Locations.Values)
            {
                var locationEntity = game.CreateEntityAsync().GetAwaiter().GetResult();
                bool startingLocation = false;
                if (location.ID == GameConfig.StartingLocationID)
                {
                    startingLocation = true;
                }

                RegisterComponentAsync(locationEntity, location, location.ExplorableLocations.ToDictionary(x => GameConfig.Locations[x].Name, x => x), startingLocation);
            }

            //how to do this without console...
            Console.WriteLine(GameConfig.InitialGameMessage);
        }

        public override async Task CycleUpdateAsync(Func<string, Task> outputHandler)
        {
            await ResolveCommandsAsync(await _inputSystem.GetEntitiesByCommandInfoTypesAsync(typeof(ExploreCommandInfo)), outputHandler);
        }

        private Task ResolveCommandsAsync(Dictionary<SharperEntity, IUniverseCommandInfo> commandData, Func<string, Task> outputHandler)
        {
            var currentLocation = Components.First(x => x.PlayerIsHere);

            foreach (var inputEntity in commandData.Keys)
            {
                var input = ((ExploreCommandInfo)commandData[inputEntity]).InputArea.ToLower();
                int locationID;
                if (!currentLocation.ExplorableLocations.TryGetValue(input, out locationID))
                {
                    outputHandler($"Cannot Explore {input}!");
                    return Task.CompletedTask;
                }

                var newLocationComponent = Components.FirstOrDefault(x => x.Location.ID == locationID);
                if (newLocationComponent == null)
                {
                    outputHandler($"newLocationComponent cannot be found!");
                    return Task.CompletedTask;
                }

                newLocationComponent.PlayerIsHere = true;
                currentLocation.PlayerIsHere = false;

                currentLocation = newLocationComponent;
                outputHandler.Invoke(currentLocation.Location.Description);
            }

            return Task.CompletedTask;
        }
    }
}
