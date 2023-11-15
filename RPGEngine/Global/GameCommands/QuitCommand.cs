using RPGEngine.Global.GameObjects;
using RPGEngine.Global.Heartbeat;
using RPGEngine.Global.Logging;
using RPGEngine.Global.Networking;
using RPGEngine.Global.Networking.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameCommands
{
    public class QuitCommand : IGameCommand
    {
        public string GameCommandName { get; set; }

        public QuitCommand()
        {
            GameCommandName = "quit";
        }

        public void ExecuteGameCommand(string[] args, Actor actor)
        {
            if(actor is Player player && player.MyClient != null)
            {
                try
                {
                    TelnetServer.Instance.RemoveClient(player.MyClient);
                    PlayerManager.Instance.RemovePlayerFromConnected(player);
                    HeartbeatManager.Instance.GameObjectsToPulse.Remove(player);
                }
                catch(Exception ex)
                {
                    Troubleshooter.Instance.Log($"Error during player quit: {ex.Message}");
                }
            }
        }
    }
}
