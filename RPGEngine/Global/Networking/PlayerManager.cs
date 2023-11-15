using RPGEngine.Global.GameObjects;
using RPGEngine.Global.Networking.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Networking
{
    /// <summary>
    /// Manages the connections and associations between players and game clients.
    /// </summary>
    public class PlayerManager
    {
        // Singleton instance of PlayerManager
        private static readonly Lazy<PlayerManager> instance = new Lazy<PlayerManager>(() => new PlayerManager());
        public static readonly PlayerManager Instance = instance.Value;

        /// <summary>
        /// Dictionary mapping game clients to their respective actors.
        /// </summary>
        public Dictionary<GameClient, Actor> PlayersActorDictionary { get; set; } = new();

        // Private constructor for singleton pattern
        private PlayerManager()
        {

        }

        /// <summary>
        /// Adds a player and their game client to the connected players list.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <param name="client">The game client associated with the player.</param>
        public void AddPlayerToConnected(Player player, GameClient client)
        {
            PlayersActorDictionary.Add(client, player);

            // Also add the client to the TelnetServer's client list
            TelnetServer.Instance.AddClient(player.MyClient);
        }

        /// <summary>
        /// Removes a player from the connected players list.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        public void RemovePlayerFromConnected(Player player)
        {
            // Remove the player's client from the dictionary
            PlayersActorDictionary.Remove(player.MyClient);
        }
    }
}
