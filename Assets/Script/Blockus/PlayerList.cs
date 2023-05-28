using System.Collections.Generic;

namespace Assets
{
    /// <summary>
    /// Contains the list of players when switching the current scene
    /// </summary>
    public static class PlayerList
    {
        public static List<Player> Players { get; set; } = new List<Player> {
            new Player(BlokusColor.Player1, "P1"),
            new Player(BlokusColor.Player2, "P2"),

        };
    }
}
