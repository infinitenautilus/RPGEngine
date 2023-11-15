using RPGEngine.Global.Communications;
using System;
using System.Net.Sockets;

namespace RPGEngine.Global.Loader
{
    public class Launcher
    {
        private List<TcpClient> clients = new();

        public static void Main(string[] args)
        {
            Launcher launcher = new Launcher();
            
            launcher.StartSequence();
        }

        private void StartSequence()
        {
            GameServer server = new GameServer();
            
            server.Start();

        }

    }
}