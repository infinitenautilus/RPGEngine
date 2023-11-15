using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects
{
    public class Agent : GameObject
    {
        
        public Agent() : base()
        {
            ShortName = "Agent Class";
            Description = "This is the Agent Class.";
        }
        
        public Agent(string shortname, string description) : base(shortname, description)
        {

        }

        public override void Pulse()
        {
            Console.WriteLine($"Pulse called on {ShortName}");
        }

    }
}
