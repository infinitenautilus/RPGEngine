using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Spawner
{
    public class AgentSpawnManager
    {
        private static readonly Lazy<AgentSpawnManager> instance = new Lazy<AgentSpawnManager>(() => new AgentSpawnManager());
        public static AgentSpawnManager Instance { get { return instance.Value; } }

        private List<Agent> AgentsSpawnedList { get; set; } = new();
        
        private AgentSpawnManager()
        {

        }

        public void AddAgent(Agent agent)
        {
            AgentsSpawnedList.Add(agent);
        }

        public bool AgentExists(Agent agent)
        {
            return AgentsSpawnedList.Contains(agent);
        }

        public void RemoveAgent(Agent agent)
        {
            AgentsSpawnedList.Remove(agent);
        }

        public List<Agent> AgentsSpawnList()
        {
            return AgentsSpawnedList;
        }

        public Agent? GetAgent(int id)
        {
            for(int i = 0; i < AgentsSpawnedList.Count; i++)
            {
                if (AgentsSpawnedList[i].AgentId == id)
                    return AgentsSpawnedList[i];
            }

            return null;
        }
    }
}
