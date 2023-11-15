using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Communications
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
            switch(client.CurrentState)
            {
                case ClientState.Connecting:
                    client.SendMessage("Enter Username: ");
                    client.CurrentState = ClientState.Authenticating;
                    break;

                case ClientState.Authenticating:
                    client.Name = data.Trim();
                    client.SendMessage("Enter Password: ");
                    client.CurrentState = ClientState.Authenticated;
                    break;
                case ClientState.Authenticated:
                    //password validation will go here
                    client.SendMessage($"Welcome {client.Name}!");
                    client.CurrentState = ClientState.Playing;
                    break;
                default:
                    client.CurrentState = ClientState.Connecting;
                    break;
            }
        }
    }
}
