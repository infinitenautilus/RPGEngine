using RPGEngine.Global.Communications;
using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Heartbeat
{
    public class HeartbeatManager
    {
        private static readonly Lazy<HeartbeatManager> _heartbeatManager = new Lazy<HeartbeatManager>(() => new HeartbeatManager());
        public static readonly HeartbeatManager Instance = _heartbeatManager.Value;

        public static int TickCount = 0;
        
        public static DateTime LastTickTime;

        public List<GameObject> GameObjectsToPulse { get; set; } = new();
        public List<GameClient> GameClientsToPulse { get; set; } = new();

        private HeartbeatManager()
        {
            LastTickTime = DateTime.Now;
        }

        public void Heartbeat()
        {
            for (int i = 0;i <  GameObjectsToPulse.Count;i++)
            {
                GameObjectsToPulse[i].Pulse();
            }
            
            for(int i = 0; i < GameClientsToPulse.Count;i++)
            {
                GameClientsToPulse[i].Pulse();
            }

            TickCount++;
            LastTickTime = DateTime.Now;
        }
    }
}
