using RPGEngine.Global.GameObjects;
using RPGEngine.World.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Networking.Communications
{
    public class LoginSessionManager
    {
        private GameClient client;

        public LoginSessionManager(GameClient _client)
        {
            client = _client;
        }

        public void ProcessLogin(string data)
        {
            switch (client.CurrentState)
            {
                case ClientState.Connecting:
                    client.SendMessage("Enter Username: ");
                    client.CurrentState = ClientState.Authenticating;

                    break;

                case ClientState.Authenticating:

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
                    //password validation will go here

                    if (string.IsNullOrEmpty(data))
                    {
                        client.CurrentState = ClientState.Authenticating;
                        break;
                    }

                    client.SendMessage($"Welcome {client.Name}!");
                    client.CurrentState = ClientState.Playing;

                    Player player = new(client);
                    
                    player.ShortName = client.Name;
                    
                    player.SetCurrentRoom(TheVoid.Instance);

                    break;

                default:
                    client.CurrentState = ClientState.Connecting;

                    break;
            }
        }
    }
}
