using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects
{
    public class Agent : Actor
    {
        private static int agentId = 0;

        public int AgentId { get { return agentId; } }

        public Agent()
        {
            agentId++;
        }

        public override void Pulse()
        {
            base.Pulse();
        }
    }
}
