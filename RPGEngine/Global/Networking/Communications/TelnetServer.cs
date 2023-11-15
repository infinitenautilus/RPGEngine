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
    public class TelnetServer
    {
        private static readonly Lazy<TelnetServer> _instance = new Lazy<TelnetServer>(() => new TelnetServer(23));
        public static readonly TelnetServer Instance = _instance.Value;

        private TcpListener listener;
        private List<GameClient> clients = new();

        public TelnetServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            listener.Start();
            Console.WriteLine("Started listening for connections...");
        }

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
                            LoginSessionManager loginManager = new(client);
                            loginManager.ProcessLogin(receivedData);
                        }
                        else
                        {
                            string cleanedData = CleanTelnetInput(receivedData);

                            //Extract the first word as the command name
                            string commandName = cleanedData.Split(' ')[0];

                            if (GameCommandHandler.Instance.CommandExists(commandName))
                            {
                                string[] args = cleanedData.Split(' ').Skip(1).ToArray();

                                Actor? commandActor = null;

                                if (PlayerManager.Instance.PlayersActorDictionary.TryGetValue(client, out commandActor))
                                {
                                    GameCommandHandler.Instance.ExecuteCommand(commandName, args, commandActor);
                                }
                            }
                        }

                    }
                }
                catch (SocketException ex)
                {
                    Troubleshooter.Instance.Log($"Socket Error with client in ProcessNetworkData() :: {ex.Message}");
                    RemoveClient(client);
                }
                catch(ObjectDisposedException ex)
                {
                    Console.WriteLine($"Client terminated session, I will dispose of it now. {ex.Message}");
                    RemoveClient(client);
                }
                catch(Exception ex)
                {
                    Troubleshooter.Instance.Log($"Error with TelnetServer in ProcessNetworkData() :: {ex.Message}");
                    RemoveClient(client);
                }
            }
        }

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

        public List<GameClient> GetClients()
        {
            return clients;
        }

        public void AddClient(GameClient c)
        {
            clients.Add(c);
        }

        public void RemoveClient(GameClient c)
        {
            c.CloseConnection();
            clients.Remove(c);
        }

        public bool ContainsClient(GameClient c)
        {
            return clients.Contains(c);
        }
    }
}
