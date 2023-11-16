using RPGEngine.Global.Networking.Communications;
using RPGEngine.Global.GameObjects;
using RPGEngine.Global.GameObjects.GameComponents;
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

        public static int TickCount { get; private set; } = 0;
        public static DateTime LastTickTime;

        private HashSet<GameObject> subscribedGameObjects = new();

        private HeartbeatManager()
        {
            LastTickTime = DateTime.Now;
        }

        public void Heartbeat()
        {
            List<GameObject> temporaryGameObjects = new List<GameObject>(subscribedGameObjects);

            foreach(GameObject gob in temporaryGameObjects)
            {
                if(subscribedGameObjects.Contains(gob))
                {
                    foreach(GameComponent comp in gob.MyGameComponents)
                    {
                        comp.Pulse();
                    }

                    gob.Pulse();
                }
            }
            
            TickCount++;
            LastTickTime = DateTime.Now;
        }

        public void Subscribe(GameObject gob)
        {
            subscribedGameObjects.Add(gob);
        }

        public void Unsubscribe(GameObject gob)
        {
            subscribedGameObjects.Remove(gob);
        }
    }
}
