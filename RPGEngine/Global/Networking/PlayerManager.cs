using RPGEngine.Global.Communications;
using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Networking
{
    public class PlayerManager
    {
        private static readonly Lazy<PlayerManager> instance = new Lazy<PlayerManager>(()=> new PlayerManager());
        public static readonly PlayerManager Instance = instance.Value;

        private Dictionary<string, GameClient> playersConnected { get; set; } = new();
        
        public Dictionary<GameClient, Actor> PlayersActorDictionary { get; set; } = new();

        private PlayerManager()
        {
            
        }

        
        public void AddPlayerToConnected(Player player, GameClient client)
        {
            playersConnected.Add(player.ShortName, client);
            PlayersActorDictionary.Add(client, player);
        }
        
        public void RemovePlayerFromConnected(Player player)
        {
            playersConnected.Remove(player.ShortName);
            PlayersActorDictionary.Remove(player.MyClient);
            
        }

    }
}
