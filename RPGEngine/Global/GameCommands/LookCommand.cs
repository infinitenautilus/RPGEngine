using RPGEngine.Global.GameObjects;
using RPGEngine.Global.Helpers;
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
                GameRoom playerroom = player.GetCurrentRoom();

                StringBuilder sb = new();
                sb.AppendLine("You look around you and see...");
                sb.AppendLine($"{playerroom.ShortName}");
                sb.AppendLine($"{playerroom.Description}");

                List<string> visibleRoomExits = playerroom.RoomExits
                    .Where(exit => exit.ExitVisible)
                    .Select(exit => exit.ExitDirection)
                    .ToList();

                if(visibleRoomExits.Count > 0)
                {
                    string formattedExits = TextFormatter.FormatListWithCommas(visibleRoomExits);
                    
                    sb.AppendLine($"You notice the following exits: {formattedExits}");
                }

                player.MyClient.SendMessage(sb.ToString());
            }
        }
    }
}
