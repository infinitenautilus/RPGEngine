using RPGEngine.Global.GameObjects;
using RPGEngine.World.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Networking.Communications
{
    /// <summary>
    /// Manages the login session for a game client.
    /// </summary>
    public class LoginSessionManager
    {
        private GameClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSessionManager"/> class.
        /// </summary>
        /// <param name="_client">The game client associated with this login session.</param>
        public LoginSessionManager(GameClient _client)
        {
            client = _client;
        }

        /// <summary>
        /// Processes the login data received from the client.
        /// </summary>
        /// <param name="data">The data received from the client.</param>
        public void ProcessLogin(string data)
        {
            switch (client.CurrentState)
            {
                case ClientState.Connecting:
                    client.SendMessage("Enter Username: ");
                    client.CurrentState = ClientState.Authenticating;
                    break;

                case ClientState.Authenticating:
                    // Handle username input
                    if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
                    {
                        client.CurrentState = ClientState.Connecting;
                        break;
                    }

                    client.Name = data.Trim();
                    client.SendMessage("Enter Password: ");
                    client.CurrentState = ClientState.Authenticated;
                    break;

                case ClientState.Authenticated:
                    // Password validation will be implemented here
                    if (string.IsNullOrEmpty(data))
                    {
                        client.CurrentState = ClientState.Authenticating;
                        break;
                    }

                    // Greet the player and transition to the playing state
                    string _name = client.Name[0..1].ToUpper();
                    
                    _name += client.Name[1..];

                    client.Name = _name;
                    
                    client.SendMessage($"Welcome {_name}!");
                   
                    client.CurrentState = ClientState.Playing;

                    // Create and initialize the player object
                    Player player = new(client);
                    player.ShortName = _name;
                    player.SetCurrentRoom(TheVoid.Instance);
                    break;

                default:
                    // Reset to connecting state if an unrecognized state is encountered
                    client.CurrentState = ClientState.Connecting;
                    break;
            }
        }
    }
}
