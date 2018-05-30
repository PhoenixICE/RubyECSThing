using GuessingGame.Res;
using SharperUniverse.Core;

namespace GuessingGame.Component.Game
{
    public class MapComponent : BaseSharperComponent
    {
        public MapComponent(SharperEntity entity, Location location)
        {
            Entity = entity;
            Location = location;
        }

        public Location Location { get; set; }
    }
}