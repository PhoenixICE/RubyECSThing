﻿using System.Collections.Generic;

namespace GuessingGame.Res
{
    public static class GameConfig
    {
        public static Dictionary<int, Location> Locations = new Dictionary<int, Location>()
        {
            [1] = new Location(1, "Desert", "You wake up in some baren desert, looking around you see a road a hut and a cave.", 2, 3, 4),
            [2] = new Location(2, "Road", "You walk torwards the road, all you see is more road.", 2, 1),
            [3] = new Location(3, "Hut", "You walk inside the hut, all you see is an empty hut", 1),
            [4] = new Location(4, "Cave", "You wak inside the cave, all you see is tunnels", 5, 1),
            [5] = new Location(5, "Tunnel", "You wak inside the cave, all you see is tunnels", 5, 4)
        };
    }

    public class Location
    {
        public Location(int locationID, string name, string description, params int[] explorableLocations)
        {
            LocationID = locationID;
            Name = name;
            Description = description;
            ExplorableLocations = explorableLocations;
        }

        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] ExplorableLocations { get; set; }
    }
}