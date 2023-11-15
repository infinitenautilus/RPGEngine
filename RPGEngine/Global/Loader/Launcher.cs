using RPGEngine.Global.Communications;
using System;
using System.Net.Sockets;

namespace RPGEngine.Global.Loader
{
    public class Launcher
    {
        private const int GAMETICKINTERVAL = 1000;
        
        private List<TcpClient> clients = new();

        public static void Main(string[] args)
        {
            Launcher launcher = new Launcher();
            
            launcher.StartSequence();
        }

        private void StartSequence()
        {
            Console.WriteLine("Emergency umbrella.");

            GameServer server = new GameServer();
            
            server.Start();

        }

    }
}