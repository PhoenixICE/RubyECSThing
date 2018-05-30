using GuessingGame.Component.Game;
using GuessingGame.Component.Input;
using GuessingGame.System.Game;
using GuessingGame.System.State;
using SharperUniverse.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.System.Input
{
    public class ExploreInputSystem : BaseSharperSystem<ExploreInputComponent>
    {
        private PlayerLocationSystem _playerLocationSystem;
        private MapSystem _mapSystem;

        public ExploreInputSystem(GameRunner game) : base(game)
        {
        }

        [SharperInject]
        private void InitializeSystemRequirements(PlayerLocationSystem playerLocationSystem, MapSystem mapSystem)
        {
            _playerLocationSystem = playerLocationSystem;
            _mapSystem = mapSystem;
        }

        public async override Task CycleUpdateAsync(Func<string, Task> outputHandler)
        {
            if (!Components.Any())
                return;

            //should only ever be one ExploreInput at anytime
            var component = Components.Single();

            //only ever one player location
            var playerLocationComponent = _playerLocationSystem.Components.Single();

            //Is there a better way to do this?
            var explorableLocations = playerLocationComponent.Entity.Components.OfType<ExplorableComponent>();

            //this is where being able to index properties/fields would be useful
            var locationToExplore = explorableLocations.FirstOrDefault(x => x.Location.Name.ToLower() == component.Area.ToLower());

            if (locationToExplore != null)
            {
                await _playerLocationSystem.UnregisterComponentAsync(playerLocationComponent);

                //No idea how to make this any prettier
                var locationToExploreEntity = _mapSystem.Components.OfType<MapComponent>().Single(x => x.Location.LocationID == locationToExplore.Location.LocationID).Entity;

                await _playerLocationSystem.RegisterComponentAsync(locationToExploreEntity);
                await outputHandler.Invoke(locationToExplore.Location.Description);
            }
            else
            {
                await outputHandler.Invoke("No such location exists!");
            }

            await UnregisterComponentAsync(component);
        }
    }
}