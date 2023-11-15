using RPGEngine.Global.Communications;
using System;
using System.Net.Sockets;

namespace RPGEngine.Global.Loader
{
    public class Launcher
    {

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