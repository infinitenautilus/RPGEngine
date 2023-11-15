using RPGEngine.Global.GameObjects;
using RPGEngine.Global.Logging;
using RPGEngine.Global.Networking;
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
                    PlayerManager.Instance.RemovePlayerFromConnected(player);
                    player.MyClient.CloseConnection();
                }
                catch(Exception ex)
                {
                    Troubleshooter.Instance.Log($"Error during player quit: {ex.Message}");
                }
            }
        }
    }
}
