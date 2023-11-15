using RPGEngine.Global.Heartbeat;
using RPGEngine.Global.Networking.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Loader
{
    public class GameServer
    {
        private static readonly Lazy<GameServer> _server = new Lazy<GameServer>(() => new GameServer());
        public static readonly GameServer Instance = _server.Value;

        public const bool GAMEISRUNNING = true;

        private const int TickInterval = 1000;

        public GameServer()
        {

        }

        public void Start()
        {
            TelnetServer.Instance.Start();

            while(GAMEISRUNNING)
            {
                TelnetServer.Instance.AcceptConnections();
                TelnetServer.Instance.ProcessNetworkData();

                HeartbeatManager.Instance.Heartbeat();

                Thread.Sleep(TickInterval);
            }
        }
    }
}
