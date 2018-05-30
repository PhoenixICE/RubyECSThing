using System;
using System.Threading.Tasks;
using GuessingGame.Component.Game;
using SharperUniverse.Core;

namespace GuessingGame.System.Game
{
    public class MapSystem : BaseSharperSystem<MapComponent>
    {
        public MapSystem(GameRunner game) : base(game)
        {
        }

        public override Task CycleUpdateAsync(Func<string, Task> outputHandler)
        {
            return Task.CompletedTask;
        }
    }
}
