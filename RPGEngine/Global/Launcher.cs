﻿using RPGEngine.Global.Networking.Communications;
using RPGEngine.Global.GameCommands;
using RPGEngine.Global.Loader;
using RPGEngine.Global.Logging;
using System;
using System.Net.Sockets;

namespace RPGEngine.Global
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
            ChatCommand chatcmd = new();

            GameCommandHandler.Instance.RegisterCommand(quitcmd);
            GameCommandHandler.Instance.RegisterCommand(scorecmd);
            GameCommandHandler.Instance.RegisterCommand(chatcmd);

            GameServer.Instance.Start();
        }

    }
}