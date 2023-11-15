using RPGEngine.Global.Heartbeat;
using RPGEngine.Global.Networking.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Loader
{
    /// <summary>
    /// Represents the game server, managing the main game loop and server operations.
    /// </summary>
    public class GameServer
    {
        // Singleton instance of GameServer
        private static readonly Lazy<GameServer> _server = new Lazy<GameServer>(() => new GameServer());
        public static readonly GameServer Instance = _server.Value;

        // Constant to indicate if the game is running
        public const bool GAMEISRUNNING = true;

        // Interval for the game loop tick in milliseconds
        private const int TickInterval = 500;

        // Private constructor for singleton pattern
        private GameServer()
        {

        }

        /// <summary>
        /// Starts the game server, initializing necessary components and entering the main game loop.
        /// </summary>
        public void Start()
        {
            // Start the Telnet server for handling network connections
            TelnetServer.Instance.Start();

            // Main game loop
            while (GAMEISRUNNING)
            {
                // Accept new network connections
                TelnetServer.Instance.AcceptConnections();

                // Process incoming network data
                TelnetServer.Instance.ProcessNetworkData();

                // Perform heartbeat updates
                HeartbeatManager.Instance.Heartbeat();

                // Sleep for the tick interval before next loop iteration
                Thread.Sleep(TickInterval);
            }
        }
    }
}
