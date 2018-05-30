using GuessingGame.Component.Game;
using GuessingGame.Component.Input;
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

        public ExploreInputSystem(GameRunner game) : base(game)
        {
        }

        [SharperInject]
        private void InitializeSystemRequirements(PlayerLocationSystem playerLocationSystem)
        {
            _playerLocationSystem = playerLocationSystem;
        }

        public async override Task CycleUpdateAsync(Func<string, Task> outputHandler)
        {
            if (!Components.Any())
                return;

            //should only ever be one explore at anytime
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
                await _playerLocationSystem.RegisterComponentAsync(locationToExplore.Entity);
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