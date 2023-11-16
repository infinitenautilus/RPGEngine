using RPGEngine.Global.GameCommands;
using RPGEngine.Global.GameObjects;
using RPGEngine.Global.Logging;
using RPGEngine.Global.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Networking.Communications
{
    /// <summary>
    /// Handles network communications and client management for the game.
    /// This includes listening for new connections, processing incoming data,
    /// and managing active client sessions.
    /// </summary>
    public class TelnetServer
    {
        #region Singleton
        //Singleton Instance of the TelnetServer
        private static readonly Lazy<TelnetServer> _instance = new Lazy<TelnetServer>(() => new TelnetServer(23));
        public static readonly TelnetServer Instance = _instance.Value;
        #endregion

        #region Vars
        private TcpListener listener; // Listens for incoming TCP Connections
        private List<GameClient> clients = new(); // Holds list of connected clients

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Telnet server listening on the specified port.
        /// </summary>
        /// <param name="port">The port on which the server will listen.</param>
        public TelnetServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Starts the server listening for new connections.
        /// </summary>
        public void Start()
        {
            listener.Start();
            Console.WriteLine("Started listening for connections...");
        }

        /// <summary>
        /// Accepts new connections and adds them to the server's list of managed clients.
        /// This method should be called repeatedly to ensure new clients are processed.
        /// </summary>
        public void AcceptConnections()
        {
            while (listener.Pending())
            {
                TcpClient client = listener.AcceptTcpClient();

                GameClient gameClient = new(client);

                clients.Add(gameClient);

                Console.WriteLine($"Client connected: {client}.");

                gameClient.SendMessage($"Welcome to Taerin's Whisper...{Environment.NewLine} {Environment.NewLine}");
            }
        }

        /// <summary>
        /// Processes incoming network data from all connected clients.
        /// It handles different client states and executes commands or login proceedures.
        /// </summary>
        public void ProcessNetworkData()
        {

            foreach (GameClient client in clients.ToArray())
            {
                try
                {
                    string? receivedData = client.ReceiveData();

                    if (!string.IsNullOrEmpty(receivedData))
                    {
                        if (client.CurrentState != ClientState.Playing)
                        {
                            client.LoginManager.ProcessLogin(receivedData);
                        }
                        else
                        {
                            ProcessClientCommand(client, receivedData);
                        }

                    }
                }
                catch (SocketException ex)
                {
                    TroubleshootConnection("Socket Error", client, ex);
                }
                catch(ObjectDisposedException ex)
                {
                    TroubleshootConnection("Client terminated session", client, ex);
                }
                catch(Exception ex)
                {
                    TroubleshootConnection("General Error", client, ex);
                    
                }
            }
        }

        /// <summary>
        /// Cleans the input received from Telnet clients by removing non-printable characters
        /// and handling carriage return-line feed combinations.
        /// </summary>
        /// <param name="input">The raw input string received from the client.</param>
        /// <returns>A cleaned string with only printable characters and proper new lines.</returns>
        private string CleanTelnetInput(string input)
        {
            StringBuilder cleaned = new();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c >= 32 && c <= 126)
                {
                    cleaned.Append(c);
                }
                else if (c == '\r' && i + 1 < input.Length && input[i + 1] == '\n')
                {
                    cleaned.Append(Environment.NewLine);
                    i++;
                }
                //else if (c == '\n')
                //{
                //  cleaned.Append(Environment.NewLine);
                //}
            }

            return cleaned.ToString().Trim();
        }

        /// <summary>
        /// Retrieves the current list of connected clients.
        /// </summary>
        /// <returns>A list of connected GameClient instances.</returns>
        public List<GameClient> GetClients()
        {
            return clients;
        }

        /// <summary>
        /// Adds a client to the server's list of managed clients.
        /// </summary>
        /// <param name="c">The GameClient to add.</param>
        public void AddClient(GameClient c)
        {
            clients.Add(c);
        }

        /// <summary>
        /// Removes a client from the server, closing their connection and removing them from the list.
        /// </summary>
        /// <param name="c">The GameClient to remove.</param>
        public void RemoveClient(GameClient c)
        {
            c.CloseConnection();
            clients.Remove(c);
        }

        /// <summary>
        /// Checks if a specific client is currently managed by the server.
        /// </summary>
        /// <param name="c">The GameClient to check for.</param>
        /// <returns>True if the client is managed by the server, false otherwise.</returns>
        public bool ContainsClient(GameClient c)
        {
            return clients.Contains(c);
        }

        /// <summary>
        /// Processes a command received from a client. It extracts the command name and arguments,
        /// checks if the command exists, and executes it if so. Otherwise, sends an error message back.
        /// </summary>
        /// <param name="client">The client from whom the command was received.</param>
        /// <param name="receivedData">The raw data received from the client.</param>
        private void ProcessClientCommand(GameClient client, string receivedData)
        {
            string cleanedData = CleanTelnetInput(receivedData);
            string commandName = cleanedData.Split(' ')[0];
            string[] args = cleanedData.Split(' ').Skip(1).ToArray();

            if(GameCommandHandler.Instance.CommandExists(commandName))
            {
                if (PlayerManager.Instance.PlayersActorDictionary.TryGetValue(client, out Actor? commandActor))
                    GameCommandHandler.Instance.ExecuteCommand(commandName, args, commandActor);
            }
            else
            {
                client.SendMessage("I do not recognize that command.");
            }
        }

        /// <summary>
        /// Handles troubleshooting for client connections, logging the error and removing the client.
        /// </summary>
        /// <param name="message">A descriptive message about the issue encountered.</param>
        /// <param name="client">The client that experienced the issue.</param>
        /// <param name="ex">The exception that was thrown.</param>
        private void TroubleshootConnection(string message, GameClient client, Exception ex)
        {
            Troubleshooter.Instance.Log($"{message} with client in ProcessNetworkData() :: {ex.Message}");
            RemoveClient(client);
        }
        #endregion
    }
}
