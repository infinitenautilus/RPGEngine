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
    /// <summary>
    /// Represents a chat command in the game.
    /// </summary>
    public class ChatCommand : IGameCommand
    {
        /// <summary>
        /// Gets or sets the name of the game command.
        /// </summary>
        public string GameCommandName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatCommand"/> class.
        /// </summary>
        public ChatCommand()
        {
            GameCommandName = "chat";
        }

        /// <summary>
        /// Executes the chat command, broadcasting a message to all players.
        /// </summary>
        /// <param name="args">The arguments of the command, representing the chat message.</param>
        /// <param name="actor">The actor who executes the command.</param>
        public void ExecuteGameCommand(string[] args, Actor actor)
        {
            // Combine the args array into a single string representing the chat message
            string message = string.Join(" ", args);

            // Iterate over all players and send them the chat message
            foreach (Player p in PlayerManager.Instance.PlayersActorDictionary.Values.ToArray())
            {
                // Format and send the chat message to each player's client
                p.MyClient.SendMessage($"[Chat] {p.ShortName}: {message}{Environment.NewLine}");
            }
        }
    }
}
