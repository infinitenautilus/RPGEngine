using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RPGEngine.Global.GameCommands
{
    public class GameCommandHandler
    {
        private static readonly Lazy<GameCommandHandler> instance = new Lazy<GameCommandHandler>(() => new GameCommandHandler());
        public static readonly GameCommandHandler Instance = instance.Value;

        private readonly Dictionary<string, IGameCommand> commands;

        private GameCommandHandler()
        {
            commands = new Dictionary<string, IGameCommand>(StringComparer.OrdinalIgnoreCase);
        }

        public void RegisterCommand(IGameCommand gameCommand)
        {
            if (gameCommand == null || string.IsNullOrWhiteSpace(gameCommand.GameCommandName))
                throw new ArgumentException("Invalid command.");

            commands[gameCommand.GameCommandName] = gameCommand;
        }

        public bool ExecuteCommand(string commandName, string[] args, Actor actor)
        {
            if(commands.TryGetValue(commandName, out IGameCommand? command))
            {
                command.ExecuteGameCommand(args, actor);
                
                return true;
            }
            
            return false;
        }

        public bool CommandExists(string name)
        {
            return commands.ContainsKey(name);
        }
    }
}
