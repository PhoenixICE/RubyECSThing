using System.Collections.Generic;

namespace GuessingGame.Res
{
    public static class GameConfig
    {
        public const string InitialGameMessage = "You wake up in a baren desert, looking around you see a road a hut and a cave.";
        public const int StartingLocationID = 1;

        public static Dictionary<int, Location> Locations = new Dictionary<int, Location>()
        {
            [1] = new Location(1, "desert", "You walk torwards a baren desert, looking around you see a road a hut and a cave.", 2, 3, 4),
            [2] = new Location(2, "road", "You walk torwards the road, all you see is more road.", 2, 1),
            [3] = new Location(3, "hut", "You walk inside the hut, all you see is an empty hut", 1),
            [4] = new Location(4, "cave", "You wak inside the cave, all you see is tunnels", 5, 1),
            [5] = new Location(5, "tunnel", "You wak inside the cave, all you see is tunnels", 5, 4)
        };
    }

    public class Location
    {
        public Location(int id, string name, string description, params int[] explorableLocations)
        {
            ID = id;
            Name = name;
            Description = description;
            ExplorableLocations = explorableLocations;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] ExplorableLocations { get; set; }
    }
}
