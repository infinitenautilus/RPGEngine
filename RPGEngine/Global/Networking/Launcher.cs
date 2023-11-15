using RPGEngine.Global.Communications;
using RPGEngine.Global.GameCommands;
using RPGEngine.Global.Logging;
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
            Troubleshooter.Instance.Log("StartSequence() initiated.");

            QuitCommand quitcmd = new();
            ScoreCommand scorecmd = new();

            GameCommandHandler.Instance.RegisterCommand(quitcmd);
            GameCommandHandler.Instance.RegisterCommand(scorecmd);

            GameServer.Instance.Start();
        }

    }
}