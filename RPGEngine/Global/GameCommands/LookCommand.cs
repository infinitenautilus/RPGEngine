using RPGEngine.Global.GameObjects;
using RPGEngine.Global.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameCommands
{
    public class LookCommand : IGameCommand
    {
        public string GameCommandName { get; set; }

        public LookCommand()
        {
            GameCommandName = "look";
        }

        public void ExecuteGameCommand(string[] args, Actor actor)
        {
            if(actor is Player player)
            {
                player.MyClient.SendMessage("You look...");
            }
        }
    }
}
