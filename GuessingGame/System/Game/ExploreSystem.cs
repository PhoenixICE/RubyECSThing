using GuessingGame.Component.Game;
using SharperUniverse.Core;
using System;
using System.Threading.Tasks;

namespace GuessingGame.System.Input
{
    public class ExplorableSystem : BaseSharperSystem<ExplorableComponent>
    {
        public ExplorableSystem(GameRunner game) : base(game)
        {
        }

        public override Task CycleUpdateAsync(Func<string, Task> outputHandler)
        {
            return Task.CompletedTask;
        }
    }
}