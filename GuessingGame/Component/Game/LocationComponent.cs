using GuessingGame.Res;
using SharperUniverse.Core;
using System.Collections.Generic;

namespace GuessingGame.Component.Game
{
    public class LocationComponent : BaseSharperComponent
    {
        public Location Location { get; set; }
        public Dictionary<string, int> ExplorableLocations { get; set; }
        public bool PlayerIsHere { get; set; }

        public LocationComponent(SharperEntity entity, Location location, Dictionary<string, int> explorableLocations, bool playerIsHere) : base(entity)
        {
            Location = location;
            PlayerIsHere = playerIsHere;
            ExplorableLocations = explorableLocations;
        }
    }
}