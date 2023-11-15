using RPGEngine.Global.Communications;
using RPGEngine.Global.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects
{
    public class Player : Actor
    {
        public GameClient MyClient { get; } 

        public Player(GameClient client) : base()
        {
            MyClient = client;

            HealthComponent.MaxHealth = 1000;
            HealthComponent.CurrentHealth = 1000;
            
            PlayerManager.Instance.AddPlayerToConnected(this, client);
        }

        public override void Pulse()
        {
            base.Pulse();
        }

        public IPEndPoint? MyIpAddress()
        {
            return MyClient.TCPClient.Client.RemoteEndPoint as IPEndPoint;
        }

        public bool IsPlayer() { return true; }
    }
}
