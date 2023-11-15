﻿using RPGEngine.Global.Networking;
using RPGEngine.Global.Networking.Communications;
using RPGEngine.World;
using RPGEngine.World.ArlithCity.Rooms;
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
        private GameRoom? MyRoom { get; set; }

        public Player(GameClient client) : base()
        {
            MyClient = client;

            HealthComponent.MaxHealth = 1000;
            HealthComponent.CurrentHealth = 1000;

            PlayerManager.Instance.AddPlayerToConnected(this, client);
        }

        public override void Pulse()
        {
            if (MyClient != null)
            {
                base.Pulse();
            }

        }

        public GameRoom GetCurrentRoom()
        {
            if(MyRoom != null)
            {
                return MyRoom;
            }
            else
            {
                return TheVoid.Instance;
            }

        }

        public void SetCurrentRoom(GameRoom room)
        {
            MyRoom = room;
        }

        public IPEndPoint? MyIpAddress()
        {
            return MyClient.TCPClient.Client.RemoteEndPoint as IPEndPoint;
        }

        public bool IsPlayer() { return true; }
    }
}
