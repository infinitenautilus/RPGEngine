using RPGEngine.Global.Heartbeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects.GameComponents
{
    public abstract class GameComponent
    {
        public string ComponentName { get; set; } = "Component";

        public GameComponent()
        {
            
        }

        public abstract void Pulse();
    }
}
