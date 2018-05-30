using GuessingGame.Res;
using SharperUniverse.Core;

namespace GuessingGame.Component.Game
{
    public class ExplorableComponent : BaseSharperComponent
    {
        public ExplorableComponent(SharperEntity entity, Location location)
        {
            Entity = entity;
            Location = location;
        }

        public Location Location { get; set; }
    }
}