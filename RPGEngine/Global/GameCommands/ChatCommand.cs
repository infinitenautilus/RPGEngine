using RPGEngine.Global.GameObjects;
using RPGEngine.Global.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RPGEngine.Global.GameCommands
{
    public class ChatCommand : IGameCommand
    {
        public string GameCommandName { get; set; }

        public ChatCommand()
        {
            GameCommandName = "chat";
        }

        public void ExecuteGameCommand(string[] args, Actor actor)
        {
            string message = string.Join(" ", args);

            foreach(Player p in PlayerManager.Instance.PlayersActorDictionary.Values.ToArray())
            {
                p.MyClient.SendMessage($"[Chat] {p.ShortName}: {message}");
            }
        }
    }
}
