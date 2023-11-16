using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RPGEngine.Global.GameCommands
{
    /// <summary>
    /// Handles the registration and execution of game commands.
    /// </summary>
    public class GameCommandHandler
    {
        // Singleton instance of GameCommandHandler
        private static readonly Lazy<GameCommandHandler> instance = new Lazy<GameCommandHandler>(() => new GameCommandHandler());
        public static readonly GameCommandHandler Instance = instance.Value;

        // Dictionary to store registered game commands
        private readonly Dictionary<string, IGameCommand> commands;

        // Private constructor for singleton pattern
        private GameCommandHandler()
        {
            commands = new Dictionary<string, IGameCommand>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Registers a new game command.
        /// </summary>
        /// <param name="gameCommand">The game command to register.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid.</exception>
        public void RegisterCommand(IGameCommand gameCommand)
        {
            if (gameCommand == null || string.IsNullOrWhiteSpace(gameCommand.GameCommandName))
                throw new ArgumentException("Invalid command.");

            commands[gameCommand.GameCommandName] = gameCommand;
            Console.WriteLine($"GameCommand {gameCommand.GameCommandName} registered!");
        }

        /// <summary>
        /// Executes a game command if it exists.
        /// </summary>
        /// <param name="commandName">The name of the command to execute.</param>
        /// <param name="args">Arguments for the command.</param>
        /// <param name="actor">The actor executing the command.</param>
        /// <returns>True if the command was executed, false otherwise.</returns>
        public bool ExecuteCommand(string commandName, string[] args, Actor actor)
        {
            if (commands.TryGetValue(commandName, out IGameCommand? command))
            {
                command.ExecuteGameCommand(args, actor);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a command with the specified name exists.
        /// </summary>
        /// <param name="name">The name of the command to check.</param>
        /// <returns>True if the command exists, false otherwise.</returns>
        public bool CommandExists(string name)
        {
            return commands.ContainsKey(name);
        }
    }
}
